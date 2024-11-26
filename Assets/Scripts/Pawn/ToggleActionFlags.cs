using UnityEngine;

namespace WinterUniverse
{
    public class ToggleActionFlags : StateMachineBehaviour
    {
        private PawnController _pawn;
        [SerializeField] private bool _addIsPerfoming = false;
        [SerializeField] private bool _removeIsPerfoming = true;
        [SerializeField] private bool _addUseGravity = true;
        [SerializeField] private bool _removeUseGravity = false;
        [SerializeField] private bool _addCanMove = true;
        [SerializeField] private bool _removeCanMove = false;
        [SerializeField] private bool _addCanRotate = true;
        [SerializeField] private bool _removeCanRotate = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _pawn = animator.GetComponent<PawnController>();
            if (_addIsPerfoming)
            {
                _pawn.IsPerfomingAction = true;
            }
            else if (_removeIsPerfoming)
            {
                _pawn.IsPerfomingAction = false;
            }
            if (_addUseGravity)
            {
                _pawn.UseGravity = true;
            }
            else if (_removeUseGravity)
            {
                _pawn.UseGravity = false;
            }
            if (_addCanMove)
            {
                _pawn.CanMove = true;
            }
            else if (_removeCanMove)
            {
                _pawn.CanMove = false;
            }
            if (_addCanRotate)
            {
                _pawn.CanRotate = true;
            }
            else if (_removeCanRotate)
            {
                _pawn.CanRotate = false;
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