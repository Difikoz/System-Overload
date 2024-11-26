using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private Transform _footRightPoint;
        [SerializeField] private Transform _footLeftPoint;
        [SerializeField] private float _baseMoveSpeed = 4f;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _maxTurnAngle = 45f;

        public Transform HeadPoint => _headPoint;
        public Transform BodyPoint => _bodyPoint;
        public Transform FootRightPoint => _footRightPoint;
        public Transform FootLeftPoint => _footLeftPoint;
        public float BaseMoveSpeed => _baseMoveSpeed;
        public float Height => _height;
        public float Radius => _radius;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnUpdate()
        {
            _animator.SetFloat("RightVelocity", _pawn.RightVelocity / _pawn.PawnStats.MoveSpeed.CurrentValue);
            _animator.SetFloat("ForwardVelocity", _pawn.ForwardVelocity / _pawn.PawnStats.MoveSpeed.CurrentValue);
            _animator.SetFloat("MoveSpeed", _pawn.PawnStats.MoveSpeed.CurrentValue);
            _animator.SetFloat("FallVelocity", _pawn.FallVelocity);
            _animator.SetFloat("TurnVelocity", _pawn.TurnVelocity / _maxTurnAngle);
            _animator.SetBool("IsMoving", _pawn.IsMoving);
            _animator.SetBool("IsGrounded", _pawn.IsGrounded);
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
            if (Physics.Raycast(_footRightPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void FootL()
        {
            if (Physics.Raycast(_footLeftPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.WorldLayer.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.GetFootstepClip(hit.transform));
            }
        }

        public void Land()
        {

        }

        private void OnAnimatorMove()
        {
            if (_pawn != null && _pawn.UseRootMotion)
            {
                _pawn.PawnLocomotion.CC.Move(_animator.deltaPosition);
                //_pawn.transform.position += _animator.deltaPosition;
                _pawn.transform.rotation *= _animator.deltaRotation;
            }
        }
    }
}