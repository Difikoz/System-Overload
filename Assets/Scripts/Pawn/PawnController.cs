using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        protected PawnAnimator _pawnAnimator;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnCombat _pawnCombat;
        protected PawnEffects _pawnEffects;
        protected PawnEquipment _pawnEquipment;
        protected PawnInventory _pawnInventory;
        protected PawnSound _pawnSound;
        protected PawnStats _pawnStats;
        protected PawnInteraction _pawnInteraction;

        protected Vector3 _moveDirection;//???

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnEffects PawnEffects => _pawnEffects;
        public PawnEquipment PawnEquipment => _pawnEquipment;
        public PawnInventory PawnInventory => _pawnInventory;
        public PawnSound PawnSound => _pawnSound;
        public PawnStats PawnStats => _pawnStats;
        public PawnInteraction InteractionModule => _pawnInteraction;
        public Vector3 MoveDirection => _moveDirection;

        //
        public Action<FactionConfig> OnFactionChanged;
        public Action OnDied;

        [HideInInspector] public string CharacterName;
        [HideInInspector] public FactionConfig Faction;

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
            _pawnEquipment = GetComponentInChildren<PawnEquipment>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnInteraction = GetComponent<PawnInteraction>();
            _pawnInventory = GetComponent<PawnInventory>();
            _pawnSound = GetComponent<PawnSound>();
            _pawnStats = GetComponent<PawnStats>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnEffects = GetComponent<PawnEffects>();
            _pawnLocomotion.Initialize();
            _pawnCombat.Initialize();
            _pawnInteraction.Initialize();
            _pawnEffects.Initialize();
            _pawnSound.Initialize();
            _pawnStats.Initialize();
            _pawnAnimator.Initialize();
            _pawnEquipment.Initialize();
            //CharacterUI = GetComponentInChildren<CharacterUI>();
            IgnoreMyOwnColliders();
            DontDestroyOnLoad(this);
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
            if (!IsDead)
            {
                PawnStats.RegenerateHealth();
                PawnStats.RegenerateEnergy();
            }
            _pawnLocomotion.HandleLocomotion();
        }

        //
        protected virtual void LateUpdate()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        public virtual void CreateCharacter(PawnSaveData data)
        {
            ChangeFaction(GameManager.StaticInstance.WorldData.GetFaction(data.Faction));
            CharacterName = data.CharacterName;
            PawnStats.CreateStats();
            PawnInventory.Initialize(data.InventoryStacks);
            PawnEquipment.ClearEquipment();
            PawnEquipment.EquipBestItems();
            PawnStats.RecalculateStats();
            PawnStats.RestoreCurrentHealth(PawnStats.HealthMax.CurrentValue);
            PawnStats.RestoreCurrentEnergy(PawnStats.EnergyMax.CurrentValue);
            PawnEquipment.ForceUpdateMeshes();
        }

        public void ChangeFaction(FactionConfig data)
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
                PawnStats.HealthCurrent = 0f;
                IsDead = true;
                if (!manualSelectDeathAnimation)
                {
                    PawnAnimator.PlayActionAnimation("Death", true);
                }
                if (source != null)
                {

                }
                PawnSound.PlayDeathClip();
                PawnCombat.SetTarget();
                PawnEquipment.CloseDamageCollider();
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
                PawnStats.RestoreCurrentHealth(PawnStats.HealthMax.CurrentValue);
                PawnStats.RestoreCurrentEnergy(PawnStats.EnergyMax.CurrentValue);
                PawnAnimator.PlayActionAnimation("Revive", true);
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