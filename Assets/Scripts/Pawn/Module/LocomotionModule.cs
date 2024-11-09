using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class LocomotionModule : MonoBehaviour
    {
        private PawnController _owner;
        [SerializeField] private Vector2 _moveInput;
        [SerializeField] private Vector3 _lookDirection;
        [SerializeField] private Vector3 _moveVelocity;
        [SerializeField] private Vector3 _fallVelocity;
        [SerializeField] private float _forwardVelocity;
        [SerializeField] private float _rightVelocity;
        private RaycastHit _groundHit;

        [HideInInspector] public CharacterController CC;

        protected abstract Vector2 GetMoveInput();
        protected abstract Vector3 GetLookDirection();

        public Vector3 MoveVelocity => _moveVelocity;

        protected virtual void Awake()
        {
            _owner = GetComponent<PawnController>();
            CC = GetComponent<CharacterController>();
        }

        public void HandleLocomotion()
        {
            if (_owner.IsDead && _owner.IsGrounded)
            {
                return;
            }
            _moveInput = GetMoveInput();
            _lookDirection = GetLookDirection();
            HandleGravity();
            HandleMovement();
            HandleRotation();
            if (_moveVelocity != Vector3.zero)
            {
                _forwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
                _rightVelocity = Vector3.Dot(_moveVelocity, transform.right);
                _owner.IsMoving = true;
            }
            else
            {
                _forwardVelocity = 0f;
                _rightVelocity = 0f;
                _owner.IsMoving = false;
            }
            _owner.AnimatorModule.UpdateAnimatorMovement(_rightVelocity, _forwardVelocity, _owner.StatModule.MoveSpeed.CurrentValue);
        }

        private void HandleGravity()
        {
            if (_owner.UseGravity)
            {
                _owner.IsGrounded = _fallVelocity.y <= 0.1f && Physics.SphereCast(transform.position + CC.center, CC.radius, Vector3.down, out _groundHit, CC.center.y, GameManager.StaticInstance.WorldLayer.ObstacleMask);
                if (_owner.IsGrounded)
                {
                    _fallVelocity.y = GameManager.StaticInstance.WorldData.Gravity / 5f;
                }
                else
                {
                    _fallVelocity.y += GameManager.StaticInstance.WorldData.Gravity * Time.deltaTime;
                }
                CC.Move(_fallVelocity * Time.deltaTime);
            }
        }

        private void HandleMovement()
        {
            if (!_owner.CanMove)
            {
                _moveVelocity = Vector3.zero;
                return;
            }
            if (_moveInput != Vector2.zero)
            {
                if (_owner.IsRunning)
                {
                    _moveInput *= 2f;
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, (transform.right * _moveInput.x + transform.forward * _moveInput.y) * 4f, 8f * Time.deltaTime);// TODO get move speed and  acceleration stat
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, 8f * Time.deltaTime);// TODO get deceleration stat
            }
            CC.Move(_moveVelocity * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (_owner.IsDead || !_owner.CanRotate)
            {
                return;
            }
            if (_lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookDirection), 4f * Time.deltaTime);// TODO get rotate speed stat
                float _rotateDirection = Vector3.Dot(_lookDirection, transform.forward);
                if (_rotateDirection > 15f)
                {
                    _owner.AnimatorModule.Animator.SetFloat("Turn Direction", 1f);// TODO
                }
                else if (_rotateDirection < -15f)
                {
                    _owner.AnimatorModule.Animator.SetFloat("Turn Direction", -1f);// TODO
                }
                else
                {
                    _owner.AnimatorModule.Animator.SetFloat("Turn Direction", 0f);// TODO
                }
            }
        }

        public bool HandleRunning()
        {
            if (!_owner.Spawned || _owner.IsDead || _owner.IsPerfomingAction || _owner.StatModule.EnergyCurrent <= 1f || _moveInput.magnitude < 0.5f)
            {
                return false;
            }
            _owner.StatModule.ReduceCurrentEnergy(4f * Time.deltaTime);// TODO get run energy cost stat
            return true;
        }

        public void TryPerformJump()
        {
            if (!_owner.Spawned || _owner.IsDead || _owner.IsPerfomingAction || !_owner.IsGrounded || _owner.StatModule.EnergyCurrent < 10f)// TODO get jump energy cost stat
            {
                return;
            }
            _fallVelocity.y = Mathf.Sqrt(2f * -2f * GameManager.StaticInstance.WorldData.Gravity);// TODO get jump power stat
            _owner.StatModule.ReduceCurrentEnergy(10f);
        }

        public void TryPerformDash()
        {
            if (!_owner.Spawned || _owner.IsDead || _owner.IsPerfomingAction || _owner.StatModule.EnergyCurrent < 10f)// TODO get dodge energy cost stat
            {
                return;
            }
            _moveVelocity = Vector3.zero;
            _owner.AnimatorModule.PlayActionAnimation("Dash Forward", true);
            _owner.StatModule.ReduceCurrentEnergy(10f);// TODO get dodge energy cost stat
            _owner.IsDashing = true;
        }
    }
}