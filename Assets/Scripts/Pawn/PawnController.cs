using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Action<FactionConfig> OnFactionChanged;
        public Action OnDied;

        protected string _characterName;
        protected FactionConfig _faction;
        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;
        protected PawnEffects _pawnEffects;
        protected PawnEquipment _pawnEquipment;
        protected PawnInteraction _pawnInteraction;
        protected PawnInventory _pawnInventory;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnSound _pawnSound;
        protected PawnStats _pawnStats;

        public string CharacterName => _characterName;
        public FactionConfig Faction => _faction;
        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnEffects PawnEffects => _pawnEffects;
        public PawnEquipment PawnEquipment => _pawnEquipment;
        public PawnInventory PawnInventory => _pawnInventory;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnSound PawnSound => _pawnSound;
        public PawnStats PawnStats => _pawnStats;
        public PawnInteraction PawnInteraction => _pawnInteraction;

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
        protected abstract Vector2 GetMoveInput();
        protected abstract Vector3 GetLookDirection();
        //
        protected virtual void Awake()
        {
            _pawnAnimator = GetComponentInChildren<PawnAnimator>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnEffects = GetComponent<PawnEffects>();
            _pawnEquipment = GetComponentInChildren<PawnEquipment>();
            _pawnInteraction = GetComponent<PawnInteraction>();
            _pawnInventory = GetComponent<PawnInventory>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnSound = GetComponent<PawnSound>();
            _pawnStats = GetComponent<PawnStats>();
            //CharacterUI = GetComponentInChildren<CharacterUI>();
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnEffects.Initialize();
            _pawnEquipment.Initialize();
            _pawnInteraction.Initialize();
            //_pawnInventory.Initialize();
            _pawnLocomotion.Initialize();
            _pawnSound.Initialize();
            _pawnStats.Initialize();
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
                _pawnStats.RegenerateHealth();
                _pawnStats.RegenerateEnergy();
                _pawnCombat.HandleTargeting();
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
            _characterName = data.CharacterName;
            _pawnStats.CreateStats();
            _pawnInventory.Initialize(data.InventoryStacks);
            _pawnEquipment.ClearEquipment();
            _pawnEquipment.EquipBestItems();
            _pawnStats.RecalculateStats();
            _pawnStats.RestoreCurrentHealth(_pawnStats.HealthMax.CurrentValue);
            _pawnStats.RestoreCurrentEnergy(_pawnStats.EnergyMax.CurrentValue);
            _pawnEquipment.ForceUpdateMeshes();
        }

        public void ChangeFaction(FactionConfig data)
        {
            _faction = data;
            OnFactionChanged?.Invoke(_faction);
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
                _pawnStats.HealthCurrent = 0f;
                IsDead = true;
                if (!manualSelectDeathAnimation)
                {
                    _pawnAnimator.PlayActionAnimation("Death", true);
                }
                if (source != null)
                {

                }
                _pawnSound.PlayDeathClip();
                _pawnCombat.SetTarget();
                _pawnEquipment.CloseDamageCollider();
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
                _pawnStats.RestoreCurrentHealth(_pawnStats.HealthMax.CurrentValue);
                _pawnStats.RestoreCurrentEnergy(_pawnStats.EnergyMax.CurrentValue);
                _pawnAnimator.PlayActionAnimation("Revive", true);
            }
        }

        private void IgnoreMyOwnColliders()
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