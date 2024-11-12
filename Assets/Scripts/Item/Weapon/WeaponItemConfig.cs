using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/Weapon/New Item")]
    public class WeaponItemConfig : ItemConfig
    {
        [Header("Weapon Information")]
        [SerializeField] private AnimatorOverrideController _controller;
        [SerializeField] private WeaponTypeConfig _weaponType;
        [Header("Local Transform Values")]
        [SerializeField] private Vector3 _localPosition;
        [SerializeField] private Quaternion _localRotation;
        [Header("Attack")]
        [SerializeField] private WeaponActionConfig _primaryAction;
        [SerializeField] private WeaponActionConfig _secondaryAction;
        [SerializeField] private List<AudioClip> _attackClips = new();
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<EffectCreator> _ownerEffects = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();
        [SerializeField] private bool _doSplashDamage = false;
        [SerializeField] private float _splashRadius = 1f;
        [Header("Modifiers")]
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [Header("For NPC")]
        [SerializeField] private float _cooldown = 1.5f;
        [SerializeField] private float _angle = 90f;
        [SerializeField] private float _minDistance = 0.5f;
        [SerializeField] private float _maxDistance = 1.5f;

        public List<StatModifierCreator> Modifiers => _modifiers;
        public AnimatorOverrideController Controller => _controller;
        public WeaponTypeConfig WeaponType => _weaponType;
        public Vector3 LocalPosition => _localPosition;
        public Quaternion LocalRotation => _localRotation;
        public WeaponActionConfig PrimaryAction => _primaryAction;
        public WeaponActionConfig SecondaryAction => _secondaryAction;
        public List<AudioClip> AttackClips => _attackClips;
        public List<DamageType> DamageTypes => _damageTypes;
        public List<EffectCreator> OwnerEffects => _ownerEffects;
        public List<EffectCreator> TargetEffects => _targetEffects;
        public bool DoSplashDamage => _doSplashDamage;
        public float SplashRadius => _splashRadius;
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
            character.PawnEquipment.EquipWeapon(this, fromInventory);
        }
    }
}