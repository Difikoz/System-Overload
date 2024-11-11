using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _runInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    Enable();
                }
                else
                {
                    Disable();
                }
            }
        }

        public void Initialize()
        {
            _inputActions = new();
            Enable();
        }

        private void Enable()
        {
            enabled = true;
            _inputActions.Enable();
            _inputActions.Character.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed += ctx => _runInput = true;
            _inputActions.Character.Run.canceled += ctx => _runInput = false;
            _inputActions.Character.Interact.performed += ctx => GameManager.StaticInstance.Player.PawnInteraction.Interact();
            _inputActions.Character.Jump.performed += ctx => GameManager.StaticInstance.Player.PawnLocomotion.TryPerformJump();
            _inputActions.Character.ActionPrimaryRight.performed += ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data, HandSlotType.Right, GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data.PrimaryAbility);
            _inputActions.Character.ActionPrimaryLeft.performed += ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data, HandSlotType.Left, GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data.PrimaryAbility);
            _inputActions.Character.ActionSecondaryRight.performed += ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data, HandSlotType.Right, GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data.SecondaryAbility);
            _inputActions.Character.ActionSecondaryLeft.performed += ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data, HandSlotType.Left, GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data.SecondaryAbility);
        }

        private void Disable()
        {
            _inputActions.Character.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.Look.performed -= ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed -= ctx => _runInput = true;
            _inputActions.Character.Run.canceled -= ctx => _runInput = false;
            _inputActions.Character.Interact.performed -= ctx => GameManager.StaticInstance.Player.PawnInteraction.Interact();
            _inputActions.Character.Jump.performed -= ctx => GameManager.StaticInstance.Player.PawnLocomotion.TryPerformJump();
            _inputActions.Character.ActionPrimaryRight.performed -= ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data, HandSlotType.Right, GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data.PrimaryAbility);
            _inputActions.Character.ActionPrimaryLeft.performed -= ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data, HandSlotType.Left, GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data.PrimaryAbility);
            _inputActions.Character.ActionSecondaryRight.performed -= ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data, HandSlotType.Right, GameManager.StaticInstance.Player.PawnEquipment.WeaponRightSlot.Data.SecondaryAbility);
            _inputActions.Character.ActionSecondaryLeft.performed -= ctx => GameManager.StaticInstance.Player.PawnCombat.UseWeaponAbility(GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data, HandSlotType.Left, GameManager.StaticInstance.Player.PawnEquipment.WeaponLeftSlot.Data.SecondaryAbility);
            _inputActions.Disable();
            enabled = false;
            _moveInput = Vector2.zero;
            _lookInput = Vector2.zero;
            _runInput = false;
        }

        public void HandleUpdate()
        {
            HandleRunInput();
        }

        private void HandleRunInput()
        {
            if (_moveInput != Vector2.zero && _runInput && GameManager.StaticInstance.Player.PawnLocomotion.HandleRunning())
            {
                GameManager.StaticInstance.Player.IsRunning = true;
            }
            else
            {
                GameManager.StaticInstance.Player.IsRunning = false;
            }
        }

        private void OnDestroy()
        {
            Disable();
        }
    }
}