using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PawnLocomotion : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _deceleration = 40f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 2f;
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;
        [SerializeField] private float _gravity = -20f;
        [SerializeField] private LayerMask _obstacleMask;

        private PawnController _pawn;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private float _forwardVelocity;
        private float _rightVelocity;
        private float _jumpTimer;
        private float _groundedTimer;
        private bool _isGrounded;
        private RaycastHit _groundHit;

        [SerializeField] private bool _attempToJumpTestButton;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void HandleLocomotion()
        {
            if (_attempToJumpTestButton)
            {
                _attempToJumpTestButton = false;
                Jump();
            }
            if (_jumpTimer > 0f && _groundedTimer > 0f)
            {
                _jumpTimer = 0f;
                _groundedTimer = 0f;
                ApplyJumpForce();
            }
            _isGrounded = _fallVelocity.y <= 0f && Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _groundHit, _cc.center.y - (_cc.radius / 2f), _obstacleMask);
            if (_isGrounded)
            {
                _groundedTimer = _timeToFall;
                _fallVelocity.y = _gravity / 10f;
            }
            else
            {
                _groundedTimer -= Time.deltaTime;
                _fallVelocity.y += _gravity * Time.deltaTime;
            }
            _jumpTimer -= Time.deltaTime;
            if (_pawn.MoveDirection != Vector3.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _pawn.MoveDirection.normalized * _maxSpeed, _acceleration * Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
            _forwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
            _rightVelocity = Vector3.Dot(_moveVelocity, transform.right);
            _pawn.PawnAnimator.SetBool("IsMoving", _moveVelocity != Vector3.zero);
            _pawn.PawnAnimator.SetFloat("ForwardVelocity", _forwardVelocity / _maxSpeed);
            _pawn.PawnAnimator.SetFloat("RightVelocity", _rightVelocity / _maxSpeed);
            _cc.Move(_moveVelocity * Time.deltaTime);
            _cc.Move(_fallVelocity * Time.deltaTime);
        }

        private void ApplyJumpForce()
        {
            _fallVelocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }

        public void Jump()
        {
            _jumpTimer = _timeToJump;
        }

        private void OnDrawGizmos()
        {
            if (_cc != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position + _cc.center + Vector3.down * (_cc.center.y - (_cc.radius / 2f)), _cc.radius);
            }
        }
    }
}