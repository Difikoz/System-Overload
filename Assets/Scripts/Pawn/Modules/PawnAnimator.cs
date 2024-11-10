using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private float _baseMoveSpeed = 4f;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;

        public float BaseMoveSpeed => _baseMoveSpeed;
        public float Height => _height;
        public float Radius => _radius;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            _animator = GetComponent<Animator>();
        }

        public void UpdateAnimatorMovement(float horizontal, float vertical, float maxSpeed)
        {
            _animator.SetFloat("RightVelocity", horizontal);
            _animator.SetFloat("ForwardVelocity", vertical);
            _animator.SetFloat("MoveSpeed", maxSpeed / _baseMoveSpeed);
            _animator.SetBool("IsGrounded", _pawn.IsGrounded);
            _animator.SetBool("IsMoving", _pawn.IsMoving);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void PlayActionAnimation(string name, bool isPerfoming, float fadeDelay = 0.1f, bool useRootMotion = true, bool canMove = false, bool canRotate = false)
        {
            if (_pawn.PawnCombat.CurrentWeapon != null)
            {
                _animator.runtimeAnimatorController = _pawn.PawnCombat.CurrentWeapon.Controller;
            }
            _animator.applyRootMotion = useRootMotion;
            _pawn.IsPerfomingAction = isPerfoming;
            _pawn.UseRootMotion = useRootMotion;
            _pawn.CanMove = canMove;
            _pawn.CanRotate = canRotate;
            _animator.CrossFade(name, fadeDelay);
        }

        public void FootR()
        {
            if (Physics.Raycast(_pawn.PawnCombat.FootRightPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void FootL()
        {
            if (Physics.Raycast(_pawn.PawnCombat.FootLeftPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void Land()
        {

        }

        private void OnAnimatorMove()
        {
            if (_pawn.UseRootMotion)
            {
                _pawn.transform.position += _animator.deltaPosition;
                _pawn.transform.rotation *= _animator.deltaRotation;
            }
        }
    }
}