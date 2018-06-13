using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    public static LoadingScreen Instance;
    public float FadeDuration = 1f; //Длительность фейда в секундах


    private GameObject _loadingScreenObject;
    private Slider _progressBar;
    private Animator _animator;
    private bool _loaded;
    private bool _isBlack;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            DestroyImmediate(this);
        }
        //находит canvas потом loading screen
        _loadingScreenObject = transform.GetChild(0).GetChild(0).gameObject;
        _loadingScreenObject.SetActive(true);
        _progressBar = GetComponentInChildren<Slider>();
        _animator = GetComponent<Animator>();
        _animator.speed = 1f / FadeDuration;
        _loadingScreenObject.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(SwitchToLevel(sceneIndex));
    }

    IEnumerator SwitchToLevel(int sceneIndex)
    {

        _loaded = false;
        _isBlack = false;

        _animator.SetTrigger("FadeToBlack");
        while (!_isBlack)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _loadingScreenObject.SetActive(true);

        StartCoroutine(LoadAsynchronusly(sceneIndex));
        _animator.SetTrigger("FadeFromBlack");

    }

    private void OnFadeToBlackComplete()
    {
        _isBlack = true;
    }

    private void OnFadeFromBlackComplete()
    {
        _isBlack = false;
    }

    IEnumerator LoadAsynchronusly(int sceneIndex)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!loadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            _progressBar.value = progress;
            yield return null;
        }
        StartCoroutine(OnLoaded());
    }

    IEnumerator OnLoaded()
    {
        if (_isBlack)
        {
            _loadingScreenObject.SetActive(false);
            yield break;
        }
        _animator.SetTrigger("FadeToBlack");
        while (!_isBlack)
        {
            yield return new WaitForSeconds(0.1f);
        }

        _loadingScreenObject.SetActive(false);
        _animator.SetTrigger("FadeFromBlack");
    }


}
