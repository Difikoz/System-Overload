using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldPlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private RaycastHit _cameraHit;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.ObjectManager.HumanPrefab).GetComponent<PawnController>();
            _pawn.Initialize();
            _pawn.OnDied += OnDeath;
            _pawn.OnRevived += OnRevive;
        }

        private void OnDestroy()
        {
            _pawn.OnDied -= OnDeath;
            _pawn.OnRevived -= OnRevive;
        }

        public void OnUpdate()
        {
            _pawn.MoveDirection = GameManager.StaticInstance.CameraManager.transform.right * GameManager.StaticInstance.InputManager.MoveInput.x + GameManager.StaticInstance.CameraManager.transform.forward * GameManager.StaticInstance.InputManager.MoveInput.y;
            if (Physics.Raycast(_pawn.PawnAnimator.HeadPoint.position, GameManager.StaticInstance.CameraManager.transform.forward, out _cameraHit, float.MaxValue, GameManager.StaticInstance.LayerManager.DetectableMask))
            {
                _pawn.LookDirection = (_cameraHit.point - _pawn.PawnAnimator.HeadPoint.position).normalized;
            }
            else
            {
                _pawn.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            }
            _pawn.IsRunning = GameManager.StaticInstance.InputManager.RunInput && _pawn.PawnLocomotion.HandleRunning();
            _pawn.IsJumping = GameManager.StaticInstance.InputManager.JumpInput;
            _pawn.IsInteracting = GameManager.StaticInstance.InputManager.InteractInput;
            _pawn.IsFiring = GameManager.StaticInstance.InputManager.FireInput;
            if (GameManager.StaticInstance.InputManager.AimInput)
            {
                _pawn.IsAiming = true;
                //zoom in camera
            }
            else
            {
                _pawn.IsAiming = false;
                //zoom out camera
            }
            transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            _pawn.OnUpdate();
        }

        private void OnDeath()
        {
            GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification("You Died");
        }

        private void OnRevive()
        {
            transform.SetPositionAndRotation(GameManager.StaticInstance.SaveLoadManager.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.SaveLoadManager.CurrentSaveData.RespawnTransform.GetRotation());
        }

        public void SaveData(ref PawnSaveData data)
        {
            data.CharacterName = _pawn.CharacterName;
            data.Faction = _pawn.Faction.DisplayName;
            data.Health = _pawn.PawnStats.HealthCurrent;
            data.Energy = _pawn.PawnStats.EnergyCurrent;
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in _pawn.PawnInventory.Stacks)
            {
                if (data.InventoryStacks.ContainsKey(stack.Item.DisplayName))
                {
                    data.InventoryStacks[stack.Item.DisplayName] += stack.Amount;
                }
                else
                {
                    data.InventoryStacks.Add(stack.Item.DisplayName, stack.Amount);
                }
            }
            data.Weapon = _pawn.PawnEquipment.WeaponSlot.Config.DisplayName;
            data.Armor = _pawn.PawnEquipment.ArmorSlot.Config.DisplayName;
            data.Transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            _pawn.CreateCharacter(data);
            _pawn.PawnStats.HealthCurrent = data.Health;
            _pawn.PawnStats.EnergyCurrent = data.Energy;
            _pawn.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.CameraManager.transform.position = _pawn.transform.position;
        }
    }
}