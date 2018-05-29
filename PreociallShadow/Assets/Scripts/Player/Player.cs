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
            if (State != PlayerBehavoirState.HandPunchDamage)
            {
                Animator.SetTrigger("HandPunch");
            }
        }

        public void MmaKick()
        {
            if (State != PlayerBehavoirState.MmaKickDamage)
            {
                Animator.SetTrigger("MmaKick");
            }            
        }

        public void MmaKick2()
        {
            if (State != PlayerBehavoirState.MmaKickDamage)
            {
                Animator.SetTrigger("MmaKick2");
            }
        }

        public void HookPunchHit()
        {
            if (State != PlayerBehavoirState.HeadHitDamage)
            {
                Animator.SetTrigger("HeadHit");
            }
        }

        public void LegPunch()
        {

        }

        public void CressentKick()
        {
            Animator.SetTrigger("CressentKick");
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

        public bool IsAtacking()
        {
            switch (State)
            {
                case PlayerBehavoirState.HookPunch:
                case PlayerBehavoirState.HandPunch:
                case PlayerBehavoirState.LeftHandPunch:
                case PlayerBehavoirState.RightHandPunch:
                case PlayerBehavoirState.MmaKick:
                case PlayerBehavoirState.MmaKick1:
                case PlayerBehavoirState.MmaKick2:
                case PlayerBehavoirState.LegPunch:
                case PlayerBehavoirState.CressentKickAttack:
                    return true;
            }

            return false;
        }

        public void Heart(PunchName punchname)
        {
            switch (punchname)
            {
                case PunchName.HookPunch:

                    HookPunchHit();
                    break;

                case PunchName.HandPunch:

                    HandPunch();
                    break;

                case PunchName.MmaKick:

                    MmaKick();
                    break;

                case PunchName.MmaKick2:

                    MmaKick2();
                    break;
            }
        }
    }
}
