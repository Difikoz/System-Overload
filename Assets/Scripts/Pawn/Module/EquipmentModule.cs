using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EquipmentModule : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private Character _owner;

        public WeaponItemData UnarmedWeapon;
        public WeaponSlot WeaponRightSlot;
        public WeaponSlot WeaponLeftSlot;
        public AbilityData SpellData;
        public List<ArmorSlot> ArmorSlots = new();

        private void OnEnable()
        {
            _owner = GetComponentInParent<Character>();
        }

        public void EquipWeapon(WeaponItemData weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (weapon == null || _owner.IsDead)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _owner.InventoryModule.RemoveItem(weapon);
            }
            HandSlotType slot = HandSlotType.Right;
            WeaponHandType currentRightHandType = WeaponHandType.OneHand;
            float currentRightHandRate = 0f;
            float currentLeftHandRate = 0f;
            if (WeaponRightSlot.Data != null)
            {
                currentRightHandType = WeaponRightSlot.Data.WeaponHandType;
                currentRightHandRate = WeaponRightSlot.Data.Price;// TODO change to Power Rate
            }
            if (WeaponLeftSlot.Data != null)
            {
                currentLeftHandRate = WeaponLeftSlot.Data.Price;// TODO change to Power Rate
            }
            if (weapon.WeaponHandType == WeaponHandType.OneHand)
            {
                if (currentRightHandRate >= currentLeftHandRate && currentRightHandType != WeaponHandType.TwoHand)
                {
                    UnequipWeapon(HandSlotType.Left, addOldToInventory);
                    WeaponLeftSlot.Equip(weapon);
                    slot = HandSlotType.Left;
                }
                else
                {
                    UnequipWeapon(HandSlotType.Right, addOldToInventory);
                    WeaponRightSlot.Equip(weapon);
                    slot = HandSlotType.Right;
                }
            }
            else if (weapon.WeaponHandType == WeaponHandType.TwoHand)
            {
                UnequipWeapon(HandSlotType.Right, addOldToInventory);
                UnequipWeapon(HandSlotType.Left, addOldToInventory);
                WeaponRightSlot.Equip(weapon);
                slot = HandSlotType.Right;
            }
            if (WeaponRightSlot.Data != UnarmedWeapon || WeaponLeftSlot.Data == null || WeaponLeftSlot.Data == UnarmedWeapon)
            {
                _owner.CombatModule.CurrentWeapon = WeaponRightSlot.Data;
                _owner.CombatModule.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _owner.CombatModule.CurrentWeapon = WeaponLeftSlot.Data;
                _owner.CombatModule.CurrentSlotType = HandSlotType.Left;
            }
            _owner.AnimatorModule.PlayActionAnimation($"Swap {slot} Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(HandSlotType slot, bool addToInventory = true)
        {
            if (slot == HandSlotType.Right)
            {
                if (WeaponRightSlot.Data == UnarmedWeapon)
                {
                    return;
                }
                if (addToInventory)
                {
                    _owner.InventoryModule.AddItem(WeaponRightSlot.Data);
                }
                WeaponRightSlot.Unequip();
                WeaponRightSlot.Equip(UnarmedWeapon);
            }
            else if (slot == HandSlotType.Left)
            {
                if (WeaponLeftSlot.Data == UnarmedWeapon)
                {
                    return;
                }
                if (addToInventory)
                {
                    _owner.InventoryModule.AddItem(WeaponLeftSlot.Data);
                }
                WeaponLeftSlot.Unequip();
                WeaponLeftSlot.Equip(UnarmedWeapon);
            }
            if (WeaponRightSlot.Data != UnarmedWeapon || WeaponLeftSlot.Data == null || WeaponLeftSlot.Data == UnarmedWeapon)
            {
                _owner.CombatModule.CurrentWeapon = WeaponRightSlot.Data;
                _owner.CombatModule.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _owner.CombatModule.CurrentWeapon = WeaponLeftSlot.Data;
                _owner.CombatModule.CurrentSlotType = HandSlotType.Left;
            }
            _owner.AnimatorModule.PlayActionAnimation($"Swap {slot} Weapon", true);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemData armor, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || _owner.IsDead)
            {
                return;
            }
            foreach (ArmorSlot slot in ArmorSlots)
            {
                if (slot.Type == armor.ArmorType)
                {
                    if (removeFromInventory)
                    {
                        _owner.InventoryModule.RemoveItem(armor);
                    }
                    UnequipArmor(slot, addOldToInventory);
                    slot.Equip(armor);
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemData armor, ArmorSlot slot, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || slot == null || armor.ArmorType != slot.Type || _owner.IsDead)
            {
                return;
            }
            if (removeFromInventory)
            {
                _owner.InventoryModule.RemoveItem(armor);
            }
            UnequipArmor(slot, addOldToInventory);
            slot.Equip(armor);
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(ArmorSlot slot, bool addToInventory = true)
        {
            if (addToInventory)
            {
                _owner.InventoryModule.AddItem(slot.Data);
            }
            slot.Unequip();
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(ArmorTypeData type, bool addToInventory = true)
        {
            foreach (ArmorSlot slot in ArmorSlots)
            {
                if (slot.Type == type)
                {
                    if (addToInventory)
                    {
                        _owner.InventoryModule.AddItem(slot.Data);
                    }
                    slot.Unequip();
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void ForceUpdateMeshes()
        {
            foreach (ArmorSlot slot in ArmorSlots)
            {
                slot.ForceUpdateMeshes();
            }
        }

        public void ClearEquipment()
        {
            UnequipWeapon(HandSlotType.Right, false);
            UnequipWeapon(HandSlotType.Left, false);
            foreach (ArmorSlot slot in ArmorSlots)
            {
                UnequipArmor(slot, false);
            }
        }

        public void EquipBestItems()
        {
            UnequipWeapon(HandSlotType.Right);
            UnequipWeapon(HandSlotType.Left);
            foreach (ArmorSlot slot in ArmorSlots)
            {
                UnequipArmor(slot);
            }
            if (_owner.InventoryModule.GetBestWeapon(out WeaponItemData weapon))
            {
                EquipWeapon(weapon);
            }
            foreach (ArmorSlot slot in ArmorSlots)
            {
                if (_owner.InventoryModule.GetBestArmor(slot.Type, out ArmorItemData armor))
                {
                    EquipArmor(armor, slot);
                }
            }
        }

        public void OpenDamageCollider()
        {
            _owner.SoundModule.PlayAttackClip();
            if (_owner.CombatModule.CurrentSlotType == HandSlotType.Right)
            {
                WeaponRightSlot.MeleeWeaponDamageCollider.EnableDamageCollider();
                _owner.SoundModule.PlaySound(WeaponRightSlot.Data.AttackClips);
            }
            else
            {
                WeaponLeftSlot.MeleeWeaponDamageCollider.EnableDamageCollider();
                _owner.SoundModule.PlaySound(WeaponLeftSlot.Data.AttackClips);
            }
        }

        public void CloseDamageCollider()
        {
            if (_owner.CombatModule.CurrentSlotType == HandSlotType.Right)
            {
                WeaponRightSlot.MeleeWeaponDamageCollider.DisableDamageCollider();
            }
            else
            {
                WeaponLeftSlot.MeleeWeaponDamageCollider.DisableDamageCollider();
            }
        }

        public void PerformRangedAttack()// TODO
        {
            _owner.SoundModule.PlayAttackClip();
            if (_owner.CombatModule.CurrentSlotType == HandSlotType.Right)
            {
                //RightHandSlot.Weapon.MeleeDamageCollider.EnableDamageCollider();
                _owner.SoundModule.PlaySound(WeaponRightSlot.Data.AttackClips);
            }
            else
            {
                //LeftHandSlot.Weapon.MeleeDamageCollider.EnableDamageCollider();
                _owner.SoundModule.PlaySound(WeaponLeftSlot.Data.AttackClips);
            }
        }
    }
}