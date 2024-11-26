using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _aimBone;
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private Transform _footRightPoint;
        [SerializeField] private Transform _footLeftPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _maxTurnAngle = 45f;

        public Transform HeadPoint => _headPoint;
        public Transform BodyPoint => _bodyPoint;
        public Transform FootRightPoint => _footRightPoint;
        public Transform FootLeftPoint => _footLeftPoint;
        public float Height => _height;
        public float Radius => _radius;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnUpdate()
        {
            _animator.SetFloat("RightVelocity", _pawn.RightVelocity);
            _animator.SetFloat("ForwardVelocity", _pawn.ForwardVelocity);
            _animator.SetFloat("MoveSpeed", _pawn.PawnStats.MoveSpeed.CurrentValue / 100f);
            _animator.SetFloat("FallVelocity", _pawn.FallVelocity);
            _animator.SetFloat("TurnVelocity", _pawn.TurnVelocity / _maxTurnAngle);
            _animator.SetBool("IsMoving", _pawn.IsMoving);
            _animator.SetBool("IsGrounded", _pawn.IsGrounded);
            if (_pawn.LookDirection != Vector3.zero)
            {
                _aimBone.rotation = Quaternion.Slerp(_aimBone.rotation, Quaternion.LookRotation(_pawn.LookDirection), _pawn.PawnStats.RotateSpeed.CurrentValue * Time.deltaTime);
            }
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void PlayActionAnimation(string name, bool isPerfoming = true, float fadeDelay = 0.1f, bool canMove = false, bool canRotate = false)
        {
            _animator.runtimeAnimatorController = _pawn.PawnEquipment.WeaponSlot.Config.Controller;
            _pawn.IsPerfomingAction = isPerfoming;
            _pawn.CanMove = canMove;
            _pawn.CanRotate = canRotate;
            _animator.CrossFade(name, fadeDelay);
        }

        public void FootR()
        {
            if (Physics.Raycast(_footRightPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.SoundManager.GetFootstepClip(hit.transform));
            }
        }

        public void FootL()
        {
            if (Physics.Raycast(_footLeftPoint.position, -transform.up, out RaycastHit hit, 0.1f, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _pawn.PawnSound.PlaySound(GameManager.StaticInstance.SoundManager.GetFootstepClip(hit.transform));
            }
        }

        public void Land()
        {

        }
    }
}