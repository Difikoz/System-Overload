using UnityEngine;

namespace WinterUniverse
{
    public class RemoveActionFlag : StateMachineBehaviour
    {
        private PawnController _owner;
        [SerializeField] private bool _removeIsPerfoming = true;
        [SerializeField] private bool _removeRootMotion = true;
        [SerializeField] private bool _restoreUseGravity = true;
        [SerializeField] private bool _restoreCanMove = true;
        [SerializeField] private bool _restoreCanRotate = true;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _owner = animator.transform.parent.GetComponent<PawnController>();
            if (_removeIsPerfoming)
            {
                _owner.IsPerfomingAction = false;
            }
            if (_removeRootMotion)
            {
                animator.applyRootMotion = false;
                _owner.UseRootMotion = false;
            }
            if (_restoreUseGravity)
            {
                _owner.UseGravity = true;
            }
            if (_restoreCanMove)
            {
                _owner.CanMove = true;
            }
            if (_restoreCanRotate)
            {
                _owner.CanRotate = true;
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}