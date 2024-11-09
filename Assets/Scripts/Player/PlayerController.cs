using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        private PlayerLocomotion _playerLocomotion;
        private PlayerInteraction _playerInteraction;

        public PlayerLocomotion PlayerLocomotion => _playerLocomotion;
        public PlayerInteraction PlayerInteraction => _playerInteraction;

        protected override void Awake()
        {
            base.Awake();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _playerInteraction = GetComponent<PlayerInteraction>();
            GameManager.StaticInstance.SetPlayer(this);
        }

        protected override void Update()
        {
            _moveDirection = Vector3.zero;
            base.Update();
        }
        //
        protected override void OnEnable()
        {
            base.OnEnable();
            PawnStats.OnHealthChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
            PawnStats.OnEnergyChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
            //StatManager.OnStatChanged += PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PawnStats.OnHealthChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
            PawnStats.OnEnergyChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
            //StatManager.OnStatChanged -= PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
        }

        public override void CreateCharacter(CharacterSaveData data)
        {
            base.CreateCharacter(data);
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
        }

        protected override IEnumerator ProcessDeathEvent()
        {
            PlayerUIManager.StaticInstance.HUD.NotificationUI.DisplayNotification("You Died");
            yield return new WaitForSeconds(5f);
            Revive();
        }

        public override void Revive()
        {
            transform.SetPositionAndRotation(GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.GetRotation());
            base.Revive();
        }

        public void SaveData(ref CharacterSaveData data)
        {
            data.CharacterName = CharacterName;
            data.Faction = Faction.DisplayName;
            data.Health = PawnStats.HealthCurrent;
            data.Energy = PawnStats.EnergyCurrent;
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in PawnInventory.Stacks)
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
            data.WeaponInRightHand = PawnEquipment.WeaponRightSlot.Data.DisplayName;
            data.WeaponInLeftHand = PawnEquipment.WeaponLeftSlot.Data.DisplayName;
            data.Transform.SetPositionAndRotation(transform.position, transform.eulerAngles);
        }

        public void LoadData(CharacterSaveData data)
        {
            ChangeFaction(GameManager.StaticInstance.WorldData.GetFaction(data.Faction));
            CharacterName = data.CharacterName;
            PawnStats.CreateStats();
            PawnStats.HealthCurrent = data.Health;
            PawnStats.EnergyCurrent = data.Energy;
            PawnInventory.Initialize(data.InventoryStacks);
            if (data.WeaponInRightHand != "Unarmed")
            {
                PawnEquipment.EquipWeapon(GameManager.StaticInstance.WorldData.GetWeapon(data.WeaponInRightHand), false, false);
            }
            else
            {
                PawnEquipment.UnequipWeapon(HandSlotType.Right, false);
            }
            if (data.WeaponInLeftHand != "Unarmed")
            {
                PawnEquipment.EquipWeapon(GameManager.StaticInstance.WorldData.GetWeapon(data.WeaponInLeftHand), false, false);
            }
            else
            {
                PawnEquipment.UnequipWeapon(HandSlotType.Left, false);
            }
            PawnStats.RecalculateStats();
            transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
            PawnEquipment.ForceUpdateMeshes();
        }
    }
}