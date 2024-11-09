using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        protected override Vector2 GetMoveInput()
        {
            return GameManager.StaticInstance.PlayerInput.MoveInput;
        }

        protected override Vector3 GetLookDirection()
        {
            return GameManager.StaticInstance.PlayerCamera.transform.forward;
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            _pawnStats.OnHealthChanged += GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetHealthValues;
            _pawnStats.OnEnergyChanged += GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetEnergyValues;
            //StatManager.OnStatChanged += PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _pawnStats.OnHealthChanged -= GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetHealthValues;
            _pawnStats.OnEnergyChanged -= GameManager.StaticInstance.PlayerUI.HUD.VitalityUI.SetEnergyValues;
            //StatManager.OnStatChanged -= PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
        }

        public override void CreateCharacter(PawnSaveData data)
        {
            base.CreateCharacter(data);
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
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
            data.Transform.SetPositionAndRotation(transform.position, transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            ChangeFaction(GameManager.StaticInstance.WorldData.GetFaction(data.Faction));
            _characterName = data.CharacterName;
            _pawnStats.CreateStats();
            _pawnStats.HealthCurrent = data.Health;
            _pawnStats.EnergyCurrent = data.Energy;
            PawnInventory.Initialize(data.InventoryStacks);
            if (data.WeaponInRightHand != "Unarmed")
            {
                _pawnEquipment.EquipWeapon(GameManager.StaticInstance.WorldData.GetWeapon(data.WeaponInRightHand), false, false);
            }
            else
            {
                _pawnEquipment.UnequipWeapon(HandSlotType.Right, false);
            }
            if (data.WeaponInLeftHand != "Unarmed")
            {
                _pawnEquipment.EquipWeapon(GameManager.StaticInstance.WorldData.GetWeapon(data.WeaponInLeftHand), false, false);
            }
            else
            {
                _pawnEquipment.UnequipWeapon(HandSlotType.Left, false);
            }
            _pawnStats.RecalculateStats();
            transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
            _pawnEquipment.ForceUpdateMeshes();
        }
    }
}