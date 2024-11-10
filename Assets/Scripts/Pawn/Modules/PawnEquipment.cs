using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;

        [SerializeField] private WeaponSlot _weaponRightSlot;
        [SerializeField] private WeaponSlot _weaponLeftSlot;
        [SerializeField] private List<ArmorSlot> _armorSlots = new();

        public WeaponSlot WeaponRightSlot => _weaponRightSlot;
        public WeaponSlot WeaponLeftSlot => _weaponLeftSlot;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            _weaponRightSlot.Initialize(_pawn);
            _weaponLeftSlot.Initialize(_pawn);
            foreach (ArmorSlot slot in _armorSlots)
            {
                slot.Initialize(_pawn);
            }
        }

        public void EquipWeapon(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (weapon == null || _pawn.IsDead)
            {
                return;
            }
            if (_weaponRightSlot.Data.Price >= _weaponLeftSlot.Data.Price)
            {
                EquipWeaponToRightSlot(weapon, removeNewFromInventory, addOldToInventory);
            }
            else
            {
                EquipWeaponToLeftSlot(weapon, removeNewFromInventory, addOldToInventory);
            }
        }

        public void EquipWeapon(WeaponItemConfig weapon, HandSlotType slot, bool removeNewFromInventory = true, bool addOldToInventory = true)// for drag and drop
        {
            if (weapon == null || _pawn.IsDead)
            {
                return;
            }
            if (slot == HandSlotType.Right)
            {
                EquipWeaponToRightSlot(weapon, removeNewFromInventory, addOldToInventory);
            }
            else
            {
                EquipWeaponToLeftSlot(weapon, removeNewFromInventory, addOldToInventory);
            }
        }

        private void EquipWeaponToRightSlot(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (addOldToInventory)
            {
                _pawn.PawnInventory.AddItem(_weaponLeftSlot.Data);
            }
            _weaponLeftSlot.Equip(weapon);
            _pawn.PawnCombat.CurrentWeapon = _weaponRightSlot.Data;
            _pawn.PawnCombat.CurrentSlotType = HandSlotType.Right;
            _pawn.PawnAnimator.PlayActionAnimation($"Swap Right Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        private void EquipWeaponToLeftSlot(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (addOldToInventory)
            {
                _pawn.PawnInventory.AddItem(_weaponRightSlot.Data);
            }
            _weaponRightSlot.Equip(weapon);
            _pawn.PawnCombat.CurrentWeapon = _weaponLeftSlot.Data;
            _pawn.PawnCombat.CurrentSlotType = HandSlotType.Left;
            _pawn.PawnAnimator.PlayActionAnimation($"Swap Left Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig armor, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || _pawn.IsDead)
            {
                return;
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == armor.ArmorType)
                {
                    if (removeFromInventory)
                    {
                        _pawn.PawnInventory.RemoveItem(armor);
                    }
                    if (addOldToInventory)
                    {
                        _pawn.PawnInventory.AddItem(slot.Data);
                    }
                    slot.Equip(armor);
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig armor, ArmorSlot slot, bool removeFromInventory = true, bool addOldToInventory = true)// for drag and drop
        {
            if (armor == null || slot == null || armor.ArmorType != slot.Type || _pawn.IsDead)
            {
                return;
            }
            if (removeFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(armor);
            }
            if (addOldToInventory)
            {
                _pawn.PawnInventory.AddItem(slot.Data);
            }
            slot.Equip(armor);
            OnEquipmentChanged?.Invoke();
        }

        public void ForceUpdateMeshes()
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                slot.ForceUpdateMeshes();
            }
        }

        public void EquipBestItems()
        {
            if (_pawn.PawnInventory.GetBestWeapon(out WeaponItemConfig weapon))
            {
                if (weapon.Price > _weaponRightSlot.Data.Price)
                {
                    EquipWeapon(weapon, HandSlotType.Right);
                }
                else if (weapon.Price > _weaponLeftSlot.Data.Price)
                {
                    EquipWeapon(weapon, HandSlotType.Left);
                }
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (_pawn.PawnInventory.GetBestArmor(slot.Type, out ArmorItemConfig armor) && armor.Price > slot.Data.Price)
                {
                    EquipArmor(armor, slot);
                }
            }
        }

        public void OpenDamageCollider()
        {
            _pawn.PawnSound.PlayAttackClip();
            if (_pawn.PawnCombat.CurrentSlotType == HandSlotType.Right)
            {
                _weaponRightSlot.DamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(_weaponRightSlot.Data.AttackClips);
            }
            else
            {
                _weaponLeftSlot.DamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(_weaponLeftSlot.Data.AttackClips);
            }
        }

        public void CloseDamageCollider()
        {
            if (_pawn.PawnCombat.CurrentSlotType == HandSlotType.Right)
            {
                _weaponRightSlot.DamageCollider.DisableDamageCollider();
            }
            else
            {
                _weaponLeftSlot.DamageCollider.DisableDamageCollider();
            }
        }
    }
}