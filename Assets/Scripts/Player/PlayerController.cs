using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        [HideInInspector] public PlayerLocomotion PlayerLocomotionManager;
        [HideInInspector] public PlayerInteraction PlayerInteractionManager;

        private bool _hasSubscription;

        protected override void Awake()
        {
            base.Awake();
            PlayerLocomotionManager = GetComponent<PlayerLocomotion>();
            PlayerInteractionManager = GetComponent<PlayerInteraction>();
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
            Subscribe();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (!_hasSubscription && Spawned)
            {
                PawnStats.OnHealthChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
                PawnStats.OnEnergyChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
                PawnStats.OnExperienceChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetExperienceValues;
                //StatManager.OnStatChanged += PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
                _hasSubscription = true;
            }
        }

        private void Unsubscribe()
        {
            if (_hasSubscription && Spawned)
            {
                PawnStats.OnHealthChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
                PawnStats.OnEnergyChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
                PawnStats.OnExperienceChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetExperienceValues;
                //StatManager.OnStatChanged -= PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
                _hasSubscription = false;
            }
        }

        public override void CreateCharacter(CharacterSaveData data)
        {
            base.CreateCharacter(data);
            GameManager.StaticInstance.PlayerCamera.transform.position = transform.position;
            Subscribe();
        }

        public override void ClearCharacter()
        {
            Unsubscribe();
            base.ClearCharacter();
        }

        protected override IEnumerator ProcessDeathEvent()
        {
            PlayerUIManager.StaticInstance.HUD.NotificationUI.DisplayNotification("You Died");
            yield return new WaitForSeconds(5f);
            Revive();
        }

        public override void Revive()
        {
            transform.SetPositionAndRotation(GameManager.StaticInstance.WorldSaveGame.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.WorldSaveGame.CurrentSaveData.RespawnTransform.GetRotation());
            base.Revive();
        }

        public void SaveData(ref CharacterSaveData data)
        {
            data.CharacterName = CharacterName;
            data.Race = Race.DisplayName;
            data.Gender = Gender.ToString();
            data.Faction = Faction.DisplayName;
            data.Level = PawnStats.Level;
            data.Experience = PawnStats.Experience;
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
            ClearCharacter();
            //ChangeRace();
            //ChangeFaction();
            ChangeRace(GameManager.StaticInstance.WorldData.GetRace(data.Race));
            if (data.Gender == "Female")
            {
                ChangeGender(Gender.Female);
            }
            else
            {
                ChangeGender(Gender.Male);
            }
            ChangeFaction(GameManager.StaticInstance.WorldData.GetFaction(data.Faction));
            LeanPool.Spawn(Race.Model, transform);// spawn model and get components
            _pawnAnimator = GetComponentInChildren<PawnAnimator>();
            _pawnEquipment = GetComponentInChildren<PawnEquipment>();
            CharacterName = data.CharacterName;
            PawnStats.Level = data.Level;
            PawnStats.Experience = data.Experience;
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
            Spawned = true;
            Subscribe();
        }
    }
}