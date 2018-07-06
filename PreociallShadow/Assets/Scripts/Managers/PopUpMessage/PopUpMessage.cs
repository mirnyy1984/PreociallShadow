using UnityEngine;

namespace Assets.Scripts.Managers.PopUpMessage
{
    public class PopUpMessage : MonoBehaviour
    {
        private Animator _dialogAnim;
        public static float DisplayTime = 3f;

        private void Start () {
            Invoke("HideMessage", DisplayTime);
        }

        private void HideMessage()
        {
            GetComponent<Animator>().SetTrigger("Hide");
            Destroy(gameObject, 2f);
        }
    }
}
