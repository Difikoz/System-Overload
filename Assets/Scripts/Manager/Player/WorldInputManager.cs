using UnityEngine;

namespace WinterUniverse
{
    public class WorldInputManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _runInput;
        private bool _jumpInput;
        private bool _interactInput;
        private bool _fireInput;
        private bool _aimInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;
        public bool RunInput => _runInput;
        public bool JumpInput => _jumpInput;
        public bool InteractInput => _interactInput;
        public bool FireInput => _fireInput;
        public bool AimInput => _aimInput;

        //private void OnApplicationFocus(bool focus)
        //{
        //    if (_inputActions != null)
        //    {
        //        if (focus)
        //        {
        //            Enable();
        //        }
        //        else
        //        {
        //            Disable();
        //        }
        //    }
        //}

        public void Initialize()
        {
            _inputActions = new();
            enabled = false;
        }

        public void Enable()
        {
            if (enabled)
            {
                return;
            }
            enabled = true;
            _inputActions.Enable();
            _inputActions.Character.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed += ctx => _runInput = true;
            _inputActions.Character.Run.canceled += ctx => _runInput = false;
            _inputActions.Character.Interact.performed += ctx => _interactInput = true;
            _inputActions.Character.Interact.canceled += ctx => _interactInput = true;
            _inputActions.Character.Jump.performed += ctx => _jumpInput = true;
            _inputActions.Character.Jump.canceled += ctx => _jumpInput = false;
            _inputActions.Character.Fire.performed += ctx => _fireInput = true;
            _inputActions.Character.Fire.canceled += ctx => _fireInput = false;
            _inputActions.Character.Aim.performed += ctx => _aimInput = true;
            _inputActions.Character.Aim.canceled += ctx => _aimInput = false;
        }

        public void Disable()
        {
            if (!enabled)
            {
                return;
            }
            _inputActions.Character.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Look.performed -= ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed -= ctx => _runInput = true;
            _inputActions.Character.Run.canceled -= ctx => _runInput = false;
            _inputActions.Character.Interact.performed -= ctx => _interactInput = true;
            _inputActions.Character.Interact.canceled -= ctx => _interactInput = true;
            _inputActions.Character.Jump.performed -= ctx => _jumpInput = true;
            _inputActions.Character.Jump.canceled -= ctx => _jumpInput = false;
            _inputActions.Character.Fire.performed -= ctx => _fireInput = true;
            _inputActions.Character.Fire.canceled -= ctx => _fireInput = false;
            _inputActions.Character.Aim.performed -= ctx => _aimInput = true;
            _inputActions.Character.Aim.canceled -= ctx => _aimInput = false;
            _inputActions.Disable();
            enabled = false;
            _moveInput = Vector2.zero;
            _lookInput = Vector2.zero;
            _runInput = false;
        }

        private void OnDestroy()
        {
            Disable();
        }
    }
}