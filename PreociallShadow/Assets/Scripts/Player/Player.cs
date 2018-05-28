using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public enum PlayerType
        {
            Human,
            Ai
        }

        public Animator Animator;
        public Rigidbody PlayerBody;
        public PlayerBehavoirState State;
        public PlayerType Type = PlayerType.Human;

        private void Update()
        {
            switch (Type)
            {
                case PlayerType.Ai:

                    CheckPlayerInput();
                    break;
            }
        }

        private void CheckPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                WalkForward();
            }

            if (Input.GetKey(KeyCode.D))
            {
                WalkBackward();
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                SetDefaultPlayerState();
            }
        }

        public void HookPunch()
        {
            Animator.SetTrigger("HookPunch");
        }

        public void HandPunch()
        {
            Animator.SetTrigger("HandPunch");
        }

        /*public void LeftHandPunch()
        {

        }

        public void RightHandPunch()
        {

        }*/

        public void LegPunch()
        {

        }

        public void CressentKick()
        {
            Animator.SetTrigger("CressentKick"); 
        }

        public void MmaKick()
        {
            Animator.SetTrigger("MmaKick"); 
        }

        public void MmaKick2()
        {
            Animator.SetTrigger("MmaKick2");
        }

        public void HookPunchHit()
        {

        }

        public void LeftHandPunchHit()
        {

        }

        public void RightHandPunchHit()
        {

        }

        public void LegPunchHit()
        {

        }

        public void CressentKickHit()
        {

        }

        public void MmaKickHit()
        {
         
        }

        public void WalkForward()
        {
            Animator.SetBool("WalkForward", true);
        }

        public void WalkBackward()
        {
            Animator.SetTrigger("WalkBackward");
        }

        public void SetDefaultPlayerState()
        {
            Animator.SetBool("WalkForward", false);
            Animator.SetBool("WalkBackward", false);
        }

        public void SetState(PlayerBehavoirState state)
        {
            State = state;
        }
    }
}
