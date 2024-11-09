using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;

        public WeaponItemConfig UnarmedWeapon;
        public WeaponSlot WeaponRightSlot;
        public WeaponSlot WeaponLeftSlot;
        public AbilityData SpellData;
        public List<ArmorSlot> ArmorSlots = new();

        public virtual void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
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
                _pawn.PawnCombat.CurrentWeapon = WeaponRightSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _pawn.PawnCombat.CurrentWeapon = WeaponLeftSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Left;
            }
            _pawn.PawnAnimator.PlayActionAnimation($"Swap {slot} Weapon", true);
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
                    _pawn.PawnInventory.AddItem(WeaponRightSlot.Data);
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
                    _pawn.PawnInventory.AddItem(WeaponLeftSlot.Data);
                }
                WeaponLeftSlot.Unequip();
                WeaponLeftSlot.Equip(UnarmedWeapon);
            }
            if (WeaponRightSlot.Data != UnarmedWeapon || WeaponLeftSlot.Data == null || WeaponLeftSlot.Data == UnarmedWeapon)
            {
                _pawn.PawnCombat.CurrentWeapon = WeaponRightSlot.Data;
                _pawn.PawnCombat.CurrentSlotType = HandSlotType.Right;
            }
            else
            {
                _pawn.PawnCombat.CurrentWeapon = WeaponLeftSlot.Data;
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
            foreach (ArmorSlot slot in ArmorSlots)
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
            foreach (ArmorSlot slot in ArmorSlots)
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
            if (_pawn.PawnInventory.GetBestWeapon(out WeaponItemConfig weapon))
            {
                EquipWeapon(weapon);
            }
            foreach (ArmorSlot slot in ArmorSlots)
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
                WeaponRightSlot.MeleeWeaponDamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(WeaponRightSlot.Data.AttackClips);
            }
            else
            {
                WeaponLeftSlot.MeleeWeaponDamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(WeaponLeftSlot.Data.AttackClips);
            }
        }

        public void CloseDamageCollider()
        {
            if (_pawn.PawnCombat.CurrentSlotType == HandSlotType.Right)
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
            _pawn.PawnSound.PlayAttackClip();
            if (_pawn.PawnCombat.CurrentSlotType == HandSlotType.Right)
            {
                //RightHandSlot.Weapon.MeleeDamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(WeaponRightSlot.Data.AttackClips);
            }
            else
            {
                //LeftHandSlot.Weapon.MeleeDamageCollider.EnableDamageCollider();
                _pawn.PawnSound.PlaySound(WeaponLeftSlot.Data.AttackClips);
            }
        }
    }
}