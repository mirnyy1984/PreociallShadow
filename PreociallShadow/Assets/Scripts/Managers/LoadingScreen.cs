using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LoadingScreen : MonoBehaviour {

        public static LoadingScreen Instance;
        public float FadeDuration = 1f; //Длительность фейда в секундах
    
        public GameObject LoadingScreenObject;
        private Slider _progressBar;
        private Animator _animator;
        private bool _isBlack;

        private void Awake()
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
            LoadingScreenObject.SetActive(true);
            _progressBar = GetComponentInChildren<Slider>();
            _animator = GetComponent<Animator>();
            _animator.speed = 1f / FadeDuration;
            LoadingScreenObject.SetActive(false);
        }

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(SwitchToLevel(sceneIndex));
        }

        private IEnumerator SwitchToLevel(int sceneIndex)
        {
            _isBlack = false;

            _animator.SetTrigger("FadeToBlack");
            while (!_isBlack)
            {
                yield return new WaitForSeconds(0.1f);
            }
            LoadingScreenObject.SetActive(true);

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

        private IEnumerator LoadAsynchronusly(int sceneIndex)
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

        private IEnumerator OnLoaded()
        {
            if (_isBlack)
            {
                LoadingScreenObject.SetActive(false);
                yield break;
            }
            _animator.SetTrigger("FadeToBlack");
            while (!_isBlack)
            {
                yield return new WaitForSeconds(0.1f);
            }

            LoadingScreenObject.SetActive(false);
            _animator.SetTrigger("FadeFromBlack");
        }


    }
}
