using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        public override Vector2 GetMoveInput()
        {
            return GameManager.StaticInstance.PlayerInput.MoveInput;
        }

        public override Vector3 GetLookDirection()
        {
            return GameManager.StaticInstance.PlayerCamera.transform.forward;
        }

        protected override IEnumerator ProcessDeathEvent()
        {
            GameManager.StaticInstance.PlayerUI.HUD.NotificationUI.DisplayNotification("You Died");
            yield return new WaitForSeconds(5f);
            Revive();
        }

        public override void Revive()
        {
            transform.SetPositionAndRotation(GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetRotation());
            base.Revive();
        }

        public void SaveData(ref PawnSaveData data)
        {
            data.CharacterName = _characterName;
            data.Faction = _faction.DisplayName;
            data.Health = _pawnStats.HealthCurrent;
            data.Energy = _pawnStats.EnergyCurrent;
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in _pawnInventory.Stacks)
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
            data.WeaponInRightHand = _pawnEquipment.WeaponRightSlot.Data.DisplayName;
            data.WeaponInLeftHand = _pawnEquipment.WeaponLeftSlot.Data.DisplayName;
            // save armors
            data.Transform.SetPositionAndRotation(transform.position, transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            if (!Created)
            {
                _pawnStats.OnHealthChanged += GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetHealthValues;
                _pawnStats.OnEnergyChanged += GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetEnergyValues;
                //StatManager.OnStatChanged += PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
            }
            CreateCharacter(data);
            _pawnStats.HealthCurrent = data.Health;
            _pawnStats.EnergyCurrent = data.Energy;
            transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            _pawnStats.OnHealthChanged -= GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetHealthValues;
            _pawnStats.OnEnergyChanged -= GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetEnergyValues;
            //StatManager.OnStatChanged -= PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
        }
    }
}