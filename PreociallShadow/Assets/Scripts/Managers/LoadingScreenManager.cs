using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class LoadingScreenManager : MonoBehaviour {

        public static LoadingScreenManager Instance;
        public float FadeDuration = 1f; //Длительность фейда в секундах
    
        public GameObject LoadingScreenGO;
        private Slider _progressBar;
        private Animator _animator;
        private bool _isBlack;

        #region Singleton

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }

#endregion

        public void Start()
        {
            LoadingScreenGO.SetActive(true);
            _progressBar = GetComponentInChildren<Slider>();
            _animator = GetComponent<Animator>();
            _animator.speed = 1f / FadeDuration;
            LoadingScreenGO.SetActive(false);
        }

        public void LoadLevelNoFade(int sceneIndex)
        {
            //TODO Анимация загрузки
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        public void LoadLevelNoFade(string sceneName)
        {
            //TODO Анимация загрузки
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(SwitchToLevel(sceneIndex));
        }
        public void LoadLevel(string sceneName)
        {
            int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
            LoadLevel(sceneIndex);
        }

        private IEnumerator SwitchToLevel(int sceneIndex)
        {
            _isBlack = false;

            _animator.SetTrigger("FadeToBlack");
            while (!_isBlack)
            {
                yield return new WaitForSeconds(0.1f);
            }
            LoadingScreenGO.SetActive(true);

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
                LoadingScreenGO.SetActive(false);
                yield break;
            }
            _animator.SetTrigger("FadeToBlack");
            while (!_isBlack)
            {
                yield return new WaitForSeconds(0.1f);
            }

            LoadingScreenGO.SetActive(false);
            _animator.SetTrigger("FadeFromBlack");
        }


    }
}
