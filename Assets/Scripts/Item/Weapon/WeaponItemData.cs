using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/Equipment/Weapon/New Item")]
    public class WeaponItemData : ItemData
    {
        [Header("Equipment Modifiers")]
        public List<StatModifierCreator> Modifiers = new();
        [Header("Weapon Information")]
        public AnimatorOverrideController Controller;
        public WeaponTypeData WeaponType;
        public WeaponHandType WeaponHandType;
        [Header("Local Transform Values")]
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
        [Header("Attack")]
        public AbilityData PrimaryAbility;
        public AbilityData SecondaryAbility;
        public List<AudioClip> AttackClips = new();
        public List<DamageType> DamageTypes = new();
        public List<EffectCreator> OwnerEffects = new();
        public List<EffectCreator> TargetEffects = new();
        [Header("Block")]
        public List<AudioClip> BlockClips = new();
        [Header("For NPC")]
        public float Cooldown = 1.5f;
        public float Angle = 90f;
        public float MinDistance = 0.5f;
        public float MaxDistance = 1.5f;

        private void OnValidate()
        {
            ItemType = ItemType.Weapon;
        }

        public override void Use(PawnController character, bool fromInventory = true)// TODO проверять CanUse перед этим методом
        {
            character.EquipmentModule.EquipWeapon(this, fromInventory);
        }
    }

    [System.Serializable]
    public class DamageType
    {
        public float Damage;
        public ElementData Element;
    }
}