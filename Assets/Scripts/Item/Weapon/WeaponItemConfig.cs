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
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<AudioClip> _fireClips = new();
        [SerializeField] private List<EffectCreator> _ownerEffects = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();
        [Header("Local Transform Values")]
        [SerializeField] private Vector3 _localPosition;
        [SerializeField] private Quaternion _localRotation;
        [Header("Fire")]
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private float _fireRate = 300f;
        [SerializeField] private float _minRange = 1f;
        [SerializeField] private float _maxRange = 100f;
        [SerializeField] private float _spread = 5f;
        [SerializeField] private int _bulletPerShot = 1;
        [SerializeField] private float _force = 50f;
        [SerializeField] private float _splashRadius = 0f;

        public List<StatModifierCreator> Modifiers => _modifiers;
        public AnimatorOverrideController Controller => _controller;
        public WeaponTypeConfig WeaponType => _weaponType;
        public List<AudioClip> ActionsClips => _fireClips;
        public List<EffectCreator> OwnerEffects => _ownerEffects;
        public List<EffectCreator> TargetEffects => _targetEffects;
        public Vector3 LocalPosition => _localPosition;
        public Quaternion LocalRotation => _localRotation;
        public List<DamageType> DamageTypes => _damageTypes;
        public float FireRate => _fireRate;
        public float MinRange => _minRange;
        public float MaxRange => _maxRange;
        public float Spread => _spread;
        public int BulletPerShot => _bulletPerShot;
        public float Force => _force;
        public float SplashRadius => _splashRadius;

        public override bool Use(PawnController character, bool fromInventory = true)
        {
            character.PawnEquipment.EquipWeapon(this, fromInventory);
            return true;
        }
    }
}