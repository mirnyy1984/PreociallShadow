using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts.Managers;

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

        public bool CanBeAttcked = true; // это нужно чтоб дать 0.4c неуязвимости после получения урона

        private HitCollider[] _hitColliders;

        private void Awake()
        {
            _hitColliders = GetComponentsInChildren<HitCollider>();
        }

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

        public void MmaKick()
        {
            Animator.SetTrigger("MmaKick");
        }

        public void MmaKick2()
        {
           Animator.SetTrigger("MmaKick2");
        }
        public void CressentKick()
        {
            Animator.SetTrigger("CressentKick");
        }

        public void LegPunch()
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
        

        public void Hurt(PlayerBehavoirState reaction)
        {
            if (!CanBeAttcked)
                return;
            
            switch (reaction)
            {
                case PlayerBehavoirState.HeadHitDamage:

                    Animator.Play("HeadHit", 0);
                    break;

                case PlayerBehavoirState.HandPunchDamage:

                    Animator.SetTrigger("HandPunchDamage");
                    break;
            }
        }

        public void Stagger(float time)
        {
            StartCoroutine(Invincibility(time));
        }

        private IEnumerator Invincibility(float time)
        {
            CanBeAttcked = false;
            yield return new WaitForSeconds(time);
            CanBeAttcked = true;
        }

        public void SetReaction(PlayerBehavoirState reaction)
        {
            foreach (HitCollider hc in _hitColliders)
            {
                hc.Reaction = reaction;
            }
        }

        public void SetDamage(float value)
        {
            //TODO: множитель урона от артефактов здесь 
            foreach (HitCollider hc in _hitColliders)
            {
                hc.Damage = value;
            }
        }

        public void RecieveDamage(float damage)
        {
            print(transform.name + " got " + damage + " damage");

            if (transform.name == "PlayerA")
            {
                BattleManager.Instance.DamagePlayer(damage);
            }
            else
            {
                BattleManager.Instance.DamageEnemy(damage);
            }
        }
    }
}
