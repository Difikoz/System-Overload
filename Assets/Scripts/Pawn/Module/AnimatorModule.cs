using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorModule : MonoBehaviour
    {
        private PawnController _owner;

        [HideInInspector] public Animator Animator;
        public float Height = 2f;
        public float Radius = 0.5f;

        [SerializeField] private float _baseMoveSpeed = 4f;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        public void UpdateAnimatorMovement(float horizontal, float vertical, float moveSpeed)
        {
            Animator.SetFloat("RightVelocity", horizontal);
            Animator.SetFloat("ForwardVelocity", vertical);
            Animator.SetFloat("MoveSpeed", moveSpeed / _baseMoveSpeed);
            Animator.SetBool("IsGrounded", _owner.IsGrounded);
            Animator.SetBool("IsMoving", _owner.IsMoving);
        }

        public void PlayActionAnimation(string name, bool isPerfoming, bool useRootMotion = true, bool canMove = false, bool canRotate = false)
        {
            if (_owner.CombatModule.CurrentWeapon != null)
            {
                Animator.runtimeAnimatorController = _owner.CombatModule.CurrentWeapon.Controller;
            }
            Animator.applyRootMotion = useRootMotion;
            _owner.IsPerfomingAction = isPerfoming;
            _owner.UseRootMotion = useRootMotion;
            _owner.CanMove = canMove;
            _owner.CanRotate = canRotate;
            Animator.CrossFade(name, 0.2f);
        }

        public void FootR()
        {
            if (Physics.Raycast(_owner.CombatModule.FootRightPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _owner.SoundModule.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void FootL()
        {
            if (Physics.Raycast(_owner.CombatModule.FootLeftPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _owner.SoundModule.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void Land()
        {

        }

        private void OnAnimatorMove()
        {
            if (_owner.UseRootMotion)
            {
                _owner.transform.position += Animator.deltaPosition;
                _owner.transform.rotation *= Animator.deltaRotation;
            }
        }
    }
}