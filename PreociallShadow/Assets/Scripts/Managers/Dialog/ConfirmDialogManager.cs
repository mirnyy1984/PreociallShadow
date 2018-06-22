using System;
using UnityEngine;

namespace Assets.Scripts.Managers.PopUpMessageManager
{
    class ConfirmDialogManager : MonoBehaviour
    {
        #region Singleton

        public static ConfirmDialogManager Instance;

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

        public Canvas MessagesCanvas;
        public GameObject ConfirmDialogYNPrefab;
        
        //Окно да/нет
        public void ConfirmDialogYN(Action yesAction, string message)
        {
            var dialogGO = Instantiate(ConfirmDialogYNPrefab, MessagesCanvas.transform);
            var dialogScript = dialogGO.GetComponent<ConfirmDialogYN>();
            dialogScript.YesAction = yesAction;
            dialogScript.SetMessage(message);
        }
    }
}
