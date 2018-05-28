using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBehavoir : StateMachineBehaviour
    {
        public PlayerBehavoirState State;
        public Player Player;
        public float HorizontalForce;
        public float VerticalForce;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Player == null)
            {
                Player = animator.gameObject.GetComponent<Player>();
            }

            Player.SetState(State);

            Player.PlayerBody.drag = 0.0f;

            //Player.PlayerBody.velocity = new Vector3(HorizontalForce, 0.0f, 0.0f);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Player.PlayerBody.velocity = new Vector3(HorizontalForce, 0.0f, 0.0f);
        }


        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Player.PlayerBody.drag = 20.0f;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
    }
}
