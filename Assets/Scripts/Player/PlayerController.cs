using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : MonoBehaviour
    {
        private PawnController _pawn;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.WorldData.PawnPrefab).GetComponent<PawnController>();
        }

        public void OnUpdate()
        {
            _pawn.MoveDirection = GameManager.StaticInstance.PlayerCamera.transform.right * GameManager.StaticInstance.PlayerInput.MoveInput.x + GameManager.StaticInstance.PlayerCamera.transform.forward * GameManager.StaticInstance.PlayerInput.MoveInput.y;
            _pawn.LookDirection = GameManager.StaticInstance.PlayerCamera.transform.forward;
            transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
        }

        private void OnDeath()
        {
            GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification("You Died");
        }

        private void OnRevive()
        {
            transform.SetPositionAndRotation(GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetRotation());
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
            data.WeaponInRightHand = _pawn.PawnEquipment.WeaponRightSlot.Config.DisplayName;
            data.WeaponInLeftHand = _pawn.PawnEquipment.WeaponLeftSlot.Config.DisplayName;
            // save armors
            data.Transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            _pawn.CreateCharacter(data);
            _pawn.PawnStats.HealthCurrent = data.Health;
            _pawn.PawnStats.EnergyCurrent = data.Energy;
            _pawn.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.PlayerCamera.transform.position = _pawn.transform.position;
        }
    }
}