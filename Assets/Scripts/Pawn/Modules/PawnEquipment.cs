using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;

        [SerializeField] private WeaponItemConfig _defaultWeapon;
        [SerializeField] private ArmorItemConfig _defaultArmor;
        [SerializeField] private WeaponSlot _weaponSlot;
        [SerializeField] private ArmorSlot _armorSlot;

        public WeaponSlot WeaponSlot => _weaponSlot;
        public ArmorSlot ArmorSlot => _armorSlot;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _weaponSlot = GetComponentInChildren<WeaponSlot>();
            _armorSlot = GetComponentInChildren<ArmorSlot>();
            _weaponSlot.Initialize(_defaultWeapon);
            _armorSlot.Initialize(_defaultArmor);
        }

        public void EquipWeapon(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (weapon == null || _pawn.IsDead)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (addOldToInventory && _weaponSlot.Config != _defaultWeapon)
            {
                _pawn.PawnInventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.Setup(weapon);
            _pawn.PawnAnimator.PlayActionAnimation($"Swap Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig armor, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || _pawn.IsDead)
            {
                return;
            }
            if (removeFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(armor);
            }
            if (addOldToInventory && _armorSlot.Config != _defaultArmor)
            {
                _pawn.PawnInventory.AddItem(_armorSlot.Config);
            }
            _armorSlot.Setup(armor);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipBestItems()
        {
            if (_pawn.PawnInventory.GetBestWeapon(out WeaponItemConfig weapon) && weapon.Rating > _weaponSlot.Config.Rating)
            {
                EquipWeapon(weapon);
            }
            if (_pawn.PawnInventory.GetBestArmor(out ArmorItemConfig armor) && armor.Rating > _armorSlot.Config.Rating)
            {
                EquipArmor(armor);
            }
        }

        public void OpenDamageCollider()
        {
            _pawn.PawnSound.PlayAttackClip();
            _weaponSlot.DamageCollider.EnableDamageCollider();
            _pawn.PawnSound.PlaySound(_weaponSlot.Config.ActionsClips);
        }

        public void CloseDamageCollider()
        {
            _weaponSlot.DamageCollider.DisableDamageCollider();
        }
    }
}