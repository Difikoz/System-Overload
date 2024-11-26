using UnityEngine;

namespace WinterUniverse
{
    public class WorldCameraManager : MonoBehaviour
    {
        [SerializeField] private Transform _verticalRoot;
        [SerializeField] private float _followSpeed = 8f;
        [SerializeField] private float _horizontalRotationSpeed = 20f;
        [SerializeField] private float _verticalRotationSpeed = 10f;
        [SerializeField] private float _minLookAngle = 75f;
        [SerializeField] private float _maxLookAngle = 75f;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 8f;

        private Camera _mainCamera;
        private Vector2 _lookInput;
        private Vector3 _camLocalPosition;
        private float _lookAngle;
        private float _cameraDefaultOffset;
        private float _cameraTargetOffset;

        public Camera MainCamera => _mainCamera;

        public void Initialize()
        {
            _mainCamera = GetComponentInChildren<Camera>();
            _cameraDefaultOffset = _mainCamera.transform.localPosition.z;
        }

        public void OnUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.StaticInstance.PlayerManager.transform.position, _followSpeed * Time.deltaTime);
            _lookInput = GameManager.StaticInstance.InputManager.LookInput;
            HandleRotation();
            HandleCollision();
        }

        private void HandleRotation()
        {
            if (_lookInput.x != 0f)
            {
                transform.Rotate(Vector3.up * _lookInput.x * _horizontalRotationSpeed * Time.deltaTime);
            }
            if (_lookInput.y != 0f)
            {
                _lookAngle = Mathf.Clamp(_lookAngle - (_lookInput.y * _verticalRotationSpeed * Time.deltaTime), -_maxLookAngle, _minLookAngle);
                _verticalRoot.localRotation = Quaternion.Euler(Vector3.right * _lookAngle);
            }
        }

        private void HandleCollision()
        {
            _cameraTargetOffset = _cameraDefaultOffset;
            Vector3 direction = (_mainCamera.transform.position - _verticalRoot.position).normalized;
            if (Physics.SphereCast(_verticalRoot.position, _collisionRadius, direction, out RaycastHit hit, Mathf.Abs(_cameraTargetOffset), GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                _cameraTargetOffset = -(Vector3.Distance(_verticalRoot.position, hit.point) - _collisionRadius);
            }
            if (Mathf.Abs(_cameraTargetOffset) < _collisionRadius)
            {
                _cameraTargetOffset = -_collisionRadius;
            }
            _camLocalPosition.z = Mathf.Lerp(_mainCamera.transform.localPosition.z, _cameraTargetOffset, _collisionAvoidanceSpeed * Time.deltaTime);
            _mainCamera.transform.localPosition = _camLocalPosition;
        }
    }
}