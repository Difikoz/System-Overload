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
            //if (enabled && _inputActions != null)
            //{
            //    if (focus)
            //    {
            //        Enable();
            //    }
            //    else
            //    {
            //        Disable();
            //    }
            //}
        }

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
            _inputActions.Camera.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed += ctx => _runInput = true;
            _inputActions.Character.Run.canceled += ctx => _runInput = false;
            _inputActions.Character.Interact.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnInteraction.Interact();
            _inputActions.Character.Jump.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnLocomotion.TryPerformJump();
            _inputActions.Character.ActionPrimaryRight.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponRightSlot.Config, AttackType.Primary, HandSlotType.Right);
            _inputActions.Character.ActionPrimaryLeft.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponLeftSlot.Config, AttackType.Primary, HandSlotType.Left);
            _inputActions.Character.ActionSecondaryRight.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponRightSlot.Config, AttackType.Secondary, HandSlotType.Right);
            _inputActions.Character.ActionSecondaryLeft.performed += ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponLeftSlot.Config, AttackType.Secondary, HandSlotType.Left);
        }

        public void Disable()
        {
            if (!enabled)
            {
                return;
            }
            _inputActions.Character.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Camera.Look.performed -= ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Character.Run.performed -= ctx => _runInput = true;
            _inputActions.Character.Run.canceled -= ctx => _runInput = false;
            _inputActions.Character.Interact.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnInteraction.Interact();
            _inputActions.Character.Jump.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnLocomotion.TryPerformJump();
            _inputActions.Character.ActionPrimaryRight.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponRightSlot.Config, AttackType.Primary, HandSlotType.Right);
            _inputActions.Character.ActionPrimaryLeft.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponLeftSlot.Config, AttackType.Primary, HandSlotType.Left);
            _inputActions.Character.ActionSecondaryRight.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponRightSlot.Config, AttackType.Secondary, HandSlotType.Right);
            _inputActions.Character.ActionSecondaryLeft.performed -= ctx => GameManager.StaticInstance.Player.Pawn.PawnCombat.UseWeaponAction(GameManager.StaticInstance.Player.Pawn.PawnEquipment.WeaponLeftSlot.Config, AttackType.Secondary, HandSlotType.Left);
            _inputActions.Disable();
            enabled = false;
            _moveInput = Vector2.zero;
            _lookInput = Vector2.zero;
            _runInput = false;
        }

        public void HandleUpdate()
        {
            if (!enabled)
            {
                return;
            }
            HandleRunInput();
        }

        private void HandleRunInput()
        {
            if (_moveInput != Vector2.zero && _runInput && GameManager.StaticInstance.Player.Pawn.PawnLocomotion.HandleRunning())
            {
                GameManager.StaticInstance.Player.Pawn.IsRunning = true;
            }
            else
            {
                GameManager.StaticInstance.Player.Pawn.IsRunning = false;
            }
        }

        private void OnDestroy()
        {
            Disable();
        }
    }
}