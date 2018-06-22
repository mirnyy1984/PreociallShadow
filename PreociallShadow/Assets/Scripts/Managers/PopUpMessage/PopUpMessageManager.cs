using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.PopUpMessage
{
    class PopUpMessageManager : MonoBehaviour
    {
        public float DisplayTime = 3f; //Время на экране

        public Canvas MessagesCanvas;
        public GameObject MessageBoxPrefab;
        public Text MessageBoxTxt;

        #region Singleton

        public static PopUpMessageManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        #endregion

        void Start()
        {
            //TODO
            ShowMessage("Уведомление");
        }

        private GameObject _dialogGo;
        private Animator _dialogAnim;
        //Уведомление
        public void ShowMessage(string message)
        {
            MessageBoxTxt.text = message;
            _dialogGo = Instantiate(MessageBoxPrefab, MessagesCanvas.transform);
            _dialogAnim = _dialogGo.GetComponent<Animator>();
            Invoke("HideMessage", DisplayTime);
        }

        private void HideMessage()
        {
            _dialogAnim.SetTrigger("Hide");
            Destroy(_dialogGo, 2f);
        }
    }
}
