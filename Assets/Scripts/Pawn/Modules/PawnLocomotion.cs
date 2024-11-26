using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PawnLocomotion : MonoBehaviour
    {
        private CharacterController _cc;
        private PawnController _pawn;
        private Vector3 _moveDirection;
        private Vector3 _lookDirection;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private float _jumpTimer;
        private float _groundedTimer;
        private RaycastHit _groundHit;

        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private float _deceleration = 4f;
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;

        private bool CanJump => _jumpTimer > 0f && _groundedTimer > 0f && !_pawn.IsDead && !_pawn.IsPerfomingAction && _pawn.PawnStats.EnergyCurrent >= _pawn.PawnStats.JumpEnergyCost.CurrentValue;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _cc = GetComponent<CharacterController>();
            _cc.height = _pawn.PawnAnimator.Height;
            _cc.radius = _pawn.PawnAnimator.Radius;
            _cc.center = _pawn.PawnAnimator.Height * Vector3.up / 2f;
        }

        public void OnUpdate()
        {
            if (_pawn.IsDead && _pawn.IsGrounded)
            {
                return;
            }
            _moveDirection = _pawn.MoveDirection;
            _lookDirection = _pawn.LookDirection;
            HandleGravity();
            HandleMovement();
            HandleRotation();
            if (_moveVelocity != Vector3.zero)
            {
                _pawn.ForwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
                _pawn.RightVelocity = Vector3.Dot(_moveVelocity, transform.right);
                _pawn.IsMoving = true;
            }
            else
            {
                _pawn.ForwardVelocity = 0f;
                _pawn.RightVelocity = 0f;
                _pawn.IsMoving = false;
            }
            _pawn.FallVelocity = _fallVelocity.y;
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
                _pawn.IsGrounded = _fallVelocity.y <= 0.1f && Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _groundHit, _cc.center.y - (_cc.radius / 2f), GameManager.StaticInstance.LayerManager.ObstacleMask);
                if (_pawn.IsGrounded)
                {
                    _groundedTimer = _timeToFall;
                    _fallVelocity.y = GameManager.StaticInstance.DataManager.Gravity / 5f;
                }
                else
                {
                    _groundedTimer -= Time.deltaTime;
                    _fallVelocity.y += GameManager.StaticInstance.DataManager.Gravity * Time.deltaTime;
                }
                _jumpTimer -= Time.deltaTime;
                _cc.Move(_fallVelocity * Time.deltaTime);
            }
        }

        private void HandleMovement()
        {
            if (!_pawn.IsGrounded)
            {
                _cc.Move(_moveVelocity * Time.deltaTime);
                return;
            }
            if (_moveDirection != Vector3.zero && _pawn.CanMove)
            {
                if (_pawn.IsRunning)
                {
                    _moveDirection *= 2f;
                }
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _moveDirection, _acceleration * Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!_pawn.CanRotate || _lookDirection == Vector3.zero)
            {
                return;
            }
            _pawn.TurnVelocity = ExtraTools.GetSignedAngleToDirection(transform.forward, _lookDirection);
            if (_pawn.IsMoving)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookDirection), _pawn.PawnStats.RotateSpeed.CurrentValue * Time.deltaTime);
            }
        }

        public bool HandleRunning()
        {
            if (_pawn.IsDead || _pawn.IsPerfomingAction || _pawn.PawnStats.EnergyCurrent <= _pawn.PawnStats.RunEnergyCost.CurrentValue || !_pawn.IsMoving)
            {
                return false;
            }
            _pawn.PawnStats.ReduceCurrentEnergy(_pawn.PawnStats.RunEnergyCost.CurrentValue * Time.deltaTime);
            return true;
        }

        public void AttempToJump()
        {
            _jumpTimer = _timeToJump;
        }

        private void ApplyJumpForce()
        {
            _fallVelocity.y = Mathf.Sqrt(_pawn.PawnStats.JumpForce.CurrentValue * -2f * GameManager.StaticInstance.DataManager.Gravity);
            _pawn.PawnStats.ReduceCurrentEnergy(_pawn.PawnStats.JumpEnergyCost.CurrentValue);
        }
    }
}