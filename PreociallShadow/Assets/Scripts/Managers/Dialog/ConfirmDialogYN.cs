using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Dialog
{
    internal class ConfirmDialogYN : MonoBehaviour
    {
        public Text Message;
        public Action YesAction;
        public void YesClick()
        {
            YesAction();
            Destroy(gameObject);
        }

        public void NoClick()
        {
            Destroy(gameObject);
        }

        public void SetMessage(string message)
        {
            Message.text = message;
        }
    }
}
