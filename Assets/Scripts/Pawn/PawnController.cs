using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        private PawnAnimator _pawnAnimator;
        private PawnLocomotion _pawnLocomotion;
        private PawnCombat _pawnCombat;

        protected Vector3 _moveDirection;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnCombat PawnCombat => _pawnCombat;
        public Vector3 MoveDirection => _moveDirection;

        //
        public Action<RaceData> OnRaceChanged;
        public Action<Gender> OnGenderChanged;
        public Action<FactionData> OnFactionChanged;
        public Action OnDied;

        [HideInInspector] public AnimatorModule AnimatorModule;
        [HideInInspector] public CombatModule CombatModule;
        [HideInInspector] public EffectModule EffectModule;
        [HideInInspector] public EquipmentModule EquipmentModule;
        [HideInInspector] public InventoryModule InventoryModule;
        [HideInInspector] public LocomotionModule LocomotionModule;
        [HideInInspector] public SoundModule SoundModule;
        [HideInInspector] public StatModule StatModule;
        [HideInInspector] public InteractionModule InteractionModule;

        [HideInInspector] public string CharacterName;
        [HideInInspector] public RaceData Race;
        [HideInInspector] public Gender Gender;
        [HideInInspector] public FactionData Faction;

        public bool Spawned;
        public bool IsPerfomingAction;
        public bool UseRootMotion;
        public bool UseGravity = true;
        public bool CanMove = true;
        public bool CanRotate = true;
        public bool CanTargeting = true;
        public bool IsTargetable = true;

        public bool IsGrounded = true;
        public bool IsMoving;
        public bool IsRunning;
        public bool IsDashing;
        public bool IsCasting;
        public bool IsTargeting;
        public bool IsInvulnerable;
        public bool IsDead;
        //

        protected virtual void Awake()
        {
            _pawnAnimator = GetComponentInChildren<PawnAnimator>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnAnimator.Initialize();
            _pawnLocomotion.Initialize();
            _pawnCombat.Initialize();
        }

        protected virtual void OnEnable()
        {
            //if (CharacterUI != null)
            //{
            //    CharacterUI.SetCharacterName(CharacterName);
            //    StatModule.OnHealthChanged += CharacterUI.SetHealthValues;
            //}
        }

        protected virtual void OnDisable()
        {
            //if (CharacterUI != null)
            //{
            //    StatModule.OnHealthChanged -= CharacterUI.SetHealthValues;
            //}
        }

        protected virtual void Update()
        {
            if (Spawned)
            {
                if (!IsDead)
                {
                    StatModule.RegenerateHealth();
                    StatModule.RegenerateEnergy();
                }
                _pawnLocomotion.HandleLocomotion();
            }
        }

        //
        protected virtual void LateUpdate()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        public virtual void CreateCharacter(CharacterSaveData data)
        {
            ClearCharacter();
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
            AnimatorModule = GetComponentInChildren<AnimatorModule>();
            CombatModule = GetComponentInChildren<CombatModule>();
            EffectModule = GetComponentInChildren<EffectModule>();
            EquipmentModule = GetComponentInChildren<EquipmentModule>();
            SoundModule = GetComponentInChildren<SoundModule>();
            StatModule = GetComponentInChildren<StatModule>();
            //CharacterUI = GetComponentInChildren<CharacterUI>();
            LocomotionModule.CC.height = AnimatorModule.Height;
            LocomotionModule.CC.radius = AnimatorModule.Radius;
            LocomotionModule.CC.center = AnimatorModule.Height * Vector3.up / 2f;
            IgnoreMyOwnColliders();
            CharacterName = data.CharacterName;
            // maybe delayed call?
            StatModule.Level = data.Level;
            StatModule.CreateStats();
            InventoryModule.CreateInventory(data.InventoryStacks);
            EquipmentModule.ClearEquipment();
            EquipmentModule.EquipBestItems();
            StatModule.RecalculateStats();
            StatModule.RestoreCurrentHealth(StatModule.HealthMax.CurrentValue);
            StatModule.RestoreCurrentEnergy(StatModule.EnergyMax.CurrentValue);
            EquipmentModule.ForceUpdateMeshes();
            Spawned = true;
        }

        public virtual void ClearCharacter()
        {
            if (Spawned)
            {
                LeanPool.Despawn(AnimatorModule.gameObject);
            }
            Spawned = false;
        }

        public void ChangeRace(RaceData data)
        {
            Race = data;
            OnRaceChanged?.Invoke(Race);
        }

        public void ChangeGender(Gender gender)
        {
            Gender = gender;
            OnGenderChanged?.Invoke(Gender);
        }

        public void ChangeFaction(FactionData data)
        {
            Faction = data;
            OnFactionChanged?.Invoke(Faction);
        }

        public virtual void EnableInvulnerable()
        {
            IsInvulnerable = true;
        }

        public virtual void DisableInvulnerable()
        {
            IsInvulnerable = false;
        }

        public virtual void Die(PawnController source = null, bool manualSelectDeathAnimation = false)
        {
            if (!IsDead)
            {
                StatModule.HealthCurrent = 0f;
                IsDead = true;
                if (!manualSelectDeathAnimation)
                {
                    AnimatorModule.PlayActionAnimation("Death", true);
                }
                if (source != null)
                {
                    if (source != null)
                    {
                        source.StatModule.AddExperience(StatModule.KillExperience);
                        Debug.Log($"Add {StatModule.KillExperience} experience to {source.gameObject.name} from {gameObject.name}");
                    }
                    //CharacterStatManager.Experience = Mathf.CeilToInt(CharacterStatManager.Experience / 2f);// терять опыт при смерти?
                }
                SoundModule.PlayDeathClip();
                CombatModule.SetTarget();
                EquipmentModule.CloseDamageCollider();
                OnDied?.Invoke();
                StartCoroutine(ProcessDeathEvent());
            }
        }

        protected virtual IEnumerator ProcessDeathEvent()
        {
            yield return new WaitForSeconds(5f);
            //LeanPool.Despawn(gameObject);
        }

        public virtual void Revive()
        {
            if (IsDead)
            {
                IsDead = false;
                StatModule.RestoreCurrentHealth(StatModule.HealthMax.CurrentValue);
                StatModule.RestoreCurrentEnergy(StatModule.EnergyMax.CurrentValue);
                AnimatorModule.PlayActionAnimation("Revive", true);
            }
        }

        protected virtual void IgnoreMyOwnColliders()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            List<Collider> ignoreColliders = new();
            foreach (Collider c in colliders)
            {
                ignoreColliders.Add(c);
            }
            ignoreColliders.Add(GetComponent<Collider>());
            foreach (Collider c in ignoreColliders)
            {
                foreach (Collider other in ignoreColliders)
                {
                    Physics.IgnoreCollision(c, other, true);
                }
            }
        }
    }
}