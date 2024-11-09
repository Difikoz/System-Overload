using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class PlayerInputManager : Singleton<PlayerInputManager>
    {
        [HideInInspector] public PlayerController Player;

        private Vector2 _moveInput;
        private bool _runInput;

        public Vector2 MoveInput => _moveInput;

        //private void OnApplicationFocus(bool focus)
        //{
        //    if (enabled)
        //    {
        //        if (focus)
        //        {
        //            _inputActions.Enable();
        //        }
        //        else
        //        {
        //            _inputActions.Disable();
        //        }
        //    }
        //}

        protected override void Awake()
        {
            base.Awake();
            SceneManager.activeSceneChanged += OnSceneChanged;
            enabled = false;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            if(newScene.buildIndex == 0)
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
        }

        private void Update()
        {
            if (Player == null)
            {
                return;
            }
            HandleRunInput();
        }

        private void HandleRunInput()
        {
            if (_moveInput != Vector2.zero && _runInput && Player.PlayerLocomotionManager.HandleRunning())
            {
                Player.IsRunning = true;
            }
            else
            {
                Player.IsRunning = false;
            }
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnRun(InputValue value)
        {
            _runInput = value.isPressed;
        }

        public void OnDash()
        {
            if (Player == null)
            {
                return;
            }
            Player.PlayerLocomotionManager.TryPerformDash();
        }

        public void OnInteract()
        {
            if (Player == null)
            {
                return;
            }
            Player.PlayerInteractionManager.Interact();
        }

        public void OnJump()
        {
            if (Player == null)
            {
                return;
            }
            Player.PlayerLocomotionManager.TryPerformJump();
        }

        public void OnActionPrimaryRight()
        {
            if (Player == null || !Player.Spawned)
            {
                return;
            }
            Player.CombatModule.UseWeaponAbility(Player.EquipmentModule.WeaponRightSlot.Data, HandSlotType.Right, Player.EquipmentModule.WeaponRightSlot.Data.PrimaryAbility);
        }

        public void OnActionPrimaryLeft()
        {
            if (Player == null || !Player.Spawned)
            {
                return;
            }
            if (Player.EquipmentModule.WeaponRightSlot.Data.WeaponHandType == WeaponHandType.TwoHand)
            {
                OnActionPrimaryRight();
            }
            else
            {
                Player.CombatModule.UseWeaponAbility(Player.EquipmentModule.WeaponLeftSlot.Data, HandSlotType.Left, Player.EquipmentModule.WeaponLeftSlot.Data.PrimaryAbility);
            }
        }

        public void OnActionSecondaryRight()
        {
            if (Player == null || !Player.Spawned)
            {
                return;
            }
            Player.CombatModule.UseWeaponAbility(Player.EquipmentModule.WeaponRightSlot.Data, HandSlotType.Right, Player.EquipmentModule.WeaponRightSlot.Data.SecondaryAbility);
        }

        public void OnActionSecondaryLeft()
        {
            if (Player == null || !Player.Spawned)
            {
                return;
            }
            if (Player.EquipmentModule.WeaponRightSlot.Data.WeaponHandType == WeaponHandType.TwoHand)
            {
                OnActionSecondaryRight();
            }
            else
            {
                Player.CombatModule.UseWeaponAbility(Player.EquipmentModule.WeaponLeftSlot.Data, HandSlotType.Left, Player.EquipmentModule.WeaponLeftSlot.Data.SecondaryAbility);
            }
        }

        public void OnCastSpell()
        {
            if (Player == null || !Player.Spawned)
            {
                return;
            }
            Player.CombatModule.UseSpellAbility(Player.EquipmentModule.SpellData);
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }
}