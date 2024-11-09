using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        [HideInInspector] public PlayerLocomotionModule PlayerLocomotionManager;
        [HideInInspector] public PlayerInteractionModule PlayerInteractionManager;

        private bool _hasSubscription;

        protected override void Awake()
        {
            base.Awake();
            PlayerLocomotionManager = GetComponent<PlayerLocomotionModule>();
            PlayerInteractionManager = GetComponent<PlayerInteractionModule>();
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
                StatModule.OnHealthChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
                StatModule.OnEnergyChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
                StatModule.OnExperienceChanged += PlayerUIManager.StaticInstance.HUD.VitalityUI.SetExperienceValues;
                //StatManager.OnStatChanged += PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
                _hasSubscription = true;
            }
        }

        private void Unsubscribe()
        {
            if (_hasSubscription && Spawned)
            {
                StatModule.OnHealthChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetHealthValues;
                StatModule.OnEnergyChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetEnergyValues;
                StatModule.OnExperienceChanged -= PlayerUIManager.StaticInstance.HUD.VitalityUI.SetExperienceValues;
                //StatManager.OnStatChanged -= PlayerUIManager.StaticInstance.MenuUI.StatUI.UpdateUI;
                _hasSubscription = false;
            }
        }

        public override void CreateCharacter(CharacterSaveData data)
        {
            base.CreateCharacter(data);
            CameraManager.StaticInstance.transform.position = transform.position;
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
            transform.SetPositionAndRotation(WorldSaveGameManager.StaticInstance.CurrentSaveData.RespawnTransform.GetPosition(), WorldSaveGameManager.StaticInstance.CurrentSaveData.RespawnTransform.GetRotation());
            base.Revive();
        }

        public void SaveData(ref CharacterSaveData data)
        {
            data.CharacterName = CharacterName;
            data.Race = Race.DisplayName;
            data.Gender = Gender.ToString();
            data.Faction = Faction.DisplayName;
            data.Level = StatModule.Level;
            data.Experience = StatModule.Experience;
            data.Health = StatModule.HealthCurrent;
            data.Energy = StatModule.EnergyCurrent;
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in InventoryModule.Stacks)
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
            data.WeaponInRightHand = EquipmentModule.WeaponRightSlot.Data.DisplayName;
            data.WeaponInLeftHand = EquipmentModule.WeaponLeftSlot.Data.DisplayName;
            data.Transform.SetPositionAndRotation(transform.position, transform.eulerAngles);
        }

        public void LoadData(CharacterSaveData data)
        {
            ClearCharacter();
            //ChangeRace();
            //ChangeFaction();
            ChangeRace(WorldDataManager.StaticInstance.GetRace(data.Race));
            if (data.Gender == "Female")
            {
                ChangeGender(Gender.Female);
            }
            else
            {
                ChangeGender(Gender.Male);
            }
            ChangeFaction(WorldDataManager.StaticInstance.GetFaction(data.Faction));
            LeanPool.Spawn(Race.Model, transform);// spawn model and get components
            AnimatorModule = GetComponentInChildren<AnimatorModule>();
            CombatModule = GetComponentInChildren<CombatModule>();
            EffectModule = GetComponentInChildren<EffectModule>();
            EquipmentModule = GetComponentInChildren<EquipmentModule>();
            SoundModule = GetComponentInChildren<SoundModule>();
            StatModule = GetComponentInChildren<StatModule>();
            CharacterName = data.CharacterName;
            StatModule.Level = data.Level;
            StatModule.Experience = data.Experience;
            StatModule.CreateStats();
            StatModule.HealthCurrent = data.Health;
            StatModule.EnergyCurrent = data.Energy;
            InventoryModule.CreateInventory(data.InventoryStacks);
            if (data.WeaponInRightHand != "Unarmed")
            {
                EquipmentModule.EquipWeapon(WorldDataManager.StaticInstance.GetWeapon(data.WeaponInRightHand), false, false);
            }
            else
            {
                EquipmentModule.UnequipWeapon(HandSlotType.Right, false);
            }
            if (data.WeaponInLeftHand != "Unarmed")
            {
                EquipmentModule.EquipWeapon(WorldDataManager.StaticInstance.GetWeapon(data.WeaponInLeftHand), false, false);
            }
            else
            {
                EquipmentModule.UnequipWeapon(HandSlotType.Left, false);
            }
            StatModule.RecalculateStats();
            transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            CameraManager.StaticInstance.transform.position = transform.position;
            EquipmentModule.ForceUpdateMeshes();
            Spawned = true;
            Subscribe();
        }
    }
}