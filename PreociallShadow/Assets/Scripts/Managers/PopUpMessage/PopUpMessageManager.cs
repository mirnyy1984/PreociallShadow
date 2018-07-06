using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.PopUpMessage
{
    internal class PopUpMessageManager : MonoBehaviour
    {
        public float DisplayTime = 3f; //Время на экране

        public Canvas MessagesCanvas;
        public GameObject MessageBoxPrefab;
        public Text MessageBoxTxt;
        public Animator Anim;

        #region Singleton

        public static PopUpMessageManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion

        private void Start()
        {
            //ShowMessage("Уведомление");
            PopUpMessage.DisplayTime = DisplayTime;
        }
        //Уведомление
        public void ShowMessage(string message)
        {
            //MessagesCanvas.gameObject.SetActive(true);
            MessageBoxTxt.text = message;
            Instantiate(MessageBoxPrefab, MessagesCanvas.transform);
        }
    }
}
