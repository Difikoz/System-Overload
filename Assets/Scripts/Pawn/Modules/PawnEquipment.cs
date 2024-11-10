using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;

        [SerializeField] private WeaponItemConfig _unarmedWeapon;
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
            HandSlotType slot = HandSlotType.Right;
            WeaponHandType currentRightHandType = WeaponHandType.OneHand;
            float currentRightHandRate = 0f;
            float currentLeftHandRate = 0f;
            if (_weaponRightSlot.Data != null)
            {
                currentRightHandType = _weaponRightSlot.Data.WeaponHandType;
                currentRightHandRate = _weaponRightSlot.Data.Price;// TODO change to Power Rate
            }
            if (_weaponLeftSlot.Data != null)
            {
                currentLeftHandRate = _weaponLeftSlot.Data.Price;// TODO change to Power Rate
            }
            if (weapon.WeaponHandType == WeaponHandType.OneHand)
            {
                if (currentRightHandRate >= currentLeftHandRate && currentRightHandType != WeaponHandType.TwoHand)
                {
                    UnequipWeapon(HandSlotType.Left, addOldToInventory);
                    _weaponLeftSlot.Equip(weapon);
                    slot = HandSlotType.Left;
                }
                else
                {
                    UnequipWeapon(HandSlotType.Right, addOldToInventory);
                    _weaponRightSlot.Equip(weapon);
                    slot = HandSlotType.Right;
                }
            }
            else if (weapon.WeaponHandType == WeaponHandType.TwoHand)
            {
                UnequipWeapon(HandSlotType.Right, addOldToInventory);
                UnequipWeapon(HandSlotType.Left, addOldToInventory);
                _weaponRightSlot.Equip(weapon);
                slot = HandSlotType.Right;
            }
            if (_weaponRightSlot.Data != _unarmedWeapon || _weaponLeftSlot.Data == null || _weaponLeftSlot.Data == _unarmedWeapon)
            {
                _pawn.PawnCombat.CurrentWeapon = _weaponRightSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _pawn.PawnCombat.CurrentWeapon = _weaponLeftSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Left;
            }
            _pawn.PawnAnimator.PlayActionAnimation($"Swap {slot} Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(HandSlotType slot, bool addToInventory = true)
        {
            if (slot == HandSlotType.Right)
            {
                if (_weaponRightSlot.Data == _unarmedWeapon)
                {
                    return;
                }
                if (addToInventory)
                {
                    _pawn.PawnInventory.AddItem(_weaponRightSlot.Data);
                }
                _weaponRightSlot.Unequip();
                _weaponRightSlot.Equip(_unarmedWeapon);
            }
            else if (slot == HandSlotType.Left)
            {
                if (_weaponLeftSlot.Data == _unarmedWeapon)
                {
                    return;
                }
                if (addToInventory)
                {
                    _pawn.PawnInventory.AddItem(_weaponLeftSlot.Data);
                }
                _weaponLeftSlot.Unequip();
                _weaponLeftSlot.Equip(_unarmedWeapon);
            }
            if (_weaponRightSlot.Data != _unarmedWeapon || _weaponLeftSlot.Data == null || _weaponLeftSlot.Data == _unarmedWeapon)
            {
                _pawn.PawnCombat.CurrentWeapon = _weaponRightSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _pawn.PawnCombat.CurrentWeapon = _weaponLeftSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Left;
            }
            _pawn.PawnAnimator.PlayActionAnimation($"Swap {slot} Weapon", true);
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
                    UnequipArmor(slot, addOldToInventory);
                    slot.Equip(armor);
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig armor, ArmorSlot slot, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || slot == null || armor.ArmorType != slot.Type || _pawn.IsDead)
            {
                return;
            }
            if (removeFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(armor);
            }
            UnequipArmor(slot, addOldToInventory);
            slot.Equip(armor);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(ArmorSlot slot, bool addToInventory = true)
        {
            if (addToInventory)
            {
                _pawn.PawnInventory.AddItem(slot.Data);
            }
            slot.Unequip();
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(ArmorTypeData type, bool addToInventory = true)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == type)
                {
                    if (addToInventory)
                    {
                        _pawn.PawnInventory.AddItem(slot.Data);
                    }
                    slot.Unequip();
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void ForceUpdateMeshes()
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                slot.ForceUpdateMeshes();
            }
        }

        public void ClearEquipment()
        {
            UnequipWeapon(HandSlotType.Right, false);
            UnequipWeapon(HandSlotType.Left, false);
            foreach (ArmorSlot slot in _armorSlots)
            {
                UnequipArmor(slot, false);
            }
        }

        public void EquipBestItems()
        {
            UnequipWeapon(HandSlotType.Right);
            UnequipWeapon(HandSlotType.Left);
            foreach (ArmorSlot slot in _armorSlots)
            {
                UnequipArmor(slot);
            }
            if (_pawn.PawnInventory.GetBestWeapon(out WeaponItemConfig weapon))
            {
                EquipWeapon(weapon);
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (_pawn.PawnInventory.GetBestArmor(slot.Type, out ArmorItemConfig armor))
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