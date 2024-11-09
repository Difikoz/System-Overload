using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class PawnLocomotion : MonoBehaviour
    {
        private CharacterController _cc;
        private PawnController _pawn;
        private Vector2 _moveInput;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private Vector3 _lookDirection;
        private float _forwardVelocity;
        private float _rightVelocity;
        private float _jumpTimer;
        private float _groundedTimer;
        private bool _isGrounded;
        private RaycastHit _groundHit;

        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _deceleration = 40f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 2f;
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;

        protected abstract Vector2 GetMoveInput();
        protected abstract Vector3 GetLookDirection();

        private bool CanJump => _jumpTimer > 0f && _groundedTimer > 0f && !_pawn.IsDead && !_pawn.IsPerfomingAction && _pawn.PawnStats.EnergyCurrent >= 10f;
        public Vector3 MoveVelocity => _moveVelocity;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _cc = GetComponent<CharacterController>();
            _cc.height = _pawn.PawnAnimator.Height;
            _cc.radius = _pawn.PawnAnimator.Radius;
            _cc.center = _pawn.PawnAnimator.Height * Vector3.up / 2f;
        }

        public void HandleLocomotion()
        {
            if (_pawn.IsDead && _pawn.IsGrounded)//???
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
                _pawn.IsMoving = true;
            }
            else
            {
                _forwardVelocity = 0f;
                _rightVelocity = 0f;
                _pawn.IsMoving = false;
            }
            _pawn.PawnAnimator.UpdateAnimatorMovement(_rightVelocity, _forwardVelocity, _pawn.PawnStats.MoveSpeed.CurrentValue);
        }

        private void HandleGravity()
        {
            if (_pawn.UseGravity)
            {
                if (CanJump)
                {
                    _jumpTimer = 0f;
                    _groundedTimer = 0f;
                    ApplyJumpForce();
                }
                _pawn.IsGrounded = _fallVelocity.y <= 0.1f && Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _groundHit, _cc.center.y - (_cc.radius / 2f), GameManager.StaticInstance.WorldLayer.ObstacleMask);
                if (_pawn.IsGrounded)
                {
                    _groundedTimer = _timeToFall;
                    _fallVelocity.y = GameManager.StaticInstance.WorldData.Gravity / 5f;
                }
                else
                {
                    _groundedTimer -= Time.deltaTime;
                    _fallVelocity.y += GameManager.StaticInstance.WorldData.Gravity * Time.deltaTime;
                }
                _jumpTimer -= Time.deltaTime;
                _cc.Move(_fallVelocity * Time.deltaTime);
            }
        }

        private void HandleMovement()
        {
            if (!_pawn.CanMove)
            {
                _moveVelocity = Vector3.zero;
                return;
            }
            if (_moveInput != Vector2.zero)
            {
                if (_pawn.IsRunning)
                {
                    _moveInput *= 2f;
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, (transform.right * _moveInput.x + transform.forward * _moveInput.y) * _maxSpeed, _acceleration * Time.deltaTime);// TODO get move speed and  acceleration stat
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);// TODO get deceleration stat
            }
            _cc.Move(_moveVelocity * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (!_pawn.CanRotate)
            {
                return;
            }
            if (_lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookDirection), 4f * Time.deltaTime);// TODO get rotate speed stat
                float _rotateDirection = Vector3.Dot(_lookDirection, transform.forward);
                if (_rotateDirection > 15f)
                {
                    _pawn.PawnAnimator.SetFloat("Turn Direction", 1f);// TODO
                }
                else if (_rotateDirection < -15f)
                {
                    _pawn.PawnAnimator.SetFloat("Turn Direction", -1f);// TODO
                }
                else
                {
                    _pawn.PawnAnimator.SetFloat("Turn Direction", 0f);// TODO
                }
            }
        }

        public bool HandleRunning()
        {
            if (_pawn.IsDead || _pawn.IsPerfomingAction || _pawn.PawnStats.EnergyCurrent <= 1f || _moveInput.magnitude < 0.5f)
            {
                return false;
            }
            _pawn.PawnStats.ReduceCurrentEnergy(4f * Time.deltaTime);// TODO get run energy cost stat
            return true;
        }

        public void TryPerformJump()
        {
            _jumpTimer = _timeToJump;
        }

        private void ApplyJumpForce()
        {
            _fallVelocity.y = Mathf.Sqrt(_jumpForce * -2f * GameManager.StaticInstance.WorldData.Gravity);
            _pawn.PawnStats.ReduceCurrentEnergy(10f);
        }

        public void TryPerformDash()
        {
            if (_pawn.IsDead || _pawn.IsPerfomingAction || _pawn.PawnStats.EnergyCurrent < 10f)// TODO get dodge energy cost stat
            {
                return;
            }
            _moveVelocity = Vector3.zero;
            _pawn.PawnAnimator.PlayActionAnimation("Dash Forward", true);
            _pawn.PawnStats.ReduceCurrentEnergy(10f);// TODO get dodge energy cost stat
            _pawn.IsDashing = true;
        }
    }
}