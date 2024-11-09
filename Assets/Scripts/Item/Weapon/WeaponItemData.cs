using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/Equipment/Weapon/New Item")]
    public class WeaponItemData : ItemData
    {
        [Header("Equipment Modifiers")]
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [Header("Weapon Information")]
        [SerializeField] private AnimatorOverrideController _controller;
        [SerializeField] private WeaponTypeData _weaponType;
        [SerializeField] private WeaponHandType _weaponHandType;
        [Header("Local Transform Values")]
        [SerializeField] private Vector3 _localPosition;
        [SerializeField] private Quaternion _localRotation;
        [Header("Attack")]
        [SerializeField] private AbilityData _primaryAbility;
        [SerializeField] private AbilityData _secondaryAbility;
        [SerializeField] private List<AudioClip> _attackClips = new();
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<EffectCreator> _ownerEffects = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();
        [Header("Block")]
        [SerializeField] private List<AudioClip> _blockClips = new();
        [Header("For NPC")]
        [SerializeField] private float _cooldown = 1.5f;
        [SerializeField] private float _angle = 90f;
        [SerializeField] private float _minDistance = 0.5f;
        [SerializeField] private float _maxDistance = 1.5f;

        public List<StatModifierCreator> Modifiers => _modifiers;
        public AnimatorOverrideController Controller => _controller;
        public WeaponTypeData WeaponType => _weaponType;
        public WeaponHandType WeaponHandType => _weaponHandType;
        public Vector3 LocalPosition => _localPosition;
        public Quaternion LocalRotation => _localRotation;
        public AbilityData PrimaryAbility => _primaryAbility;
        public AbilityData SecondaryAbility => _secondaryAbility;
        public List<AudioClip> AttackClips => _attackClips;
        public List<DamageType> DamageTypes => _damageTypes;
        public List<EffectCreator> OwnerEffects => _ownerEffects;
        public List<EffectCreator> TargetEffects => _targetEffects;
        public List<AudioClip> BlockClips => _blockClips;
        public float Cooldown => _cooldown;
        public float Angle => _angle;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }

        public override void Use(PawnController character, bool fromInventory = true)// TODO проверять CanUse перед этим методом
        {
            character.EquipmentModule.EquipWeapon(this, fromInventory);
        }
    }
}