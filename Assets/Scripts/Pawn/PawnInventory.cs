using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        public Action<List<ItemStack>> OnInventoryChanged;

        public List<ItemStack> Stacks = new();

        public virtual void Initialize(SerializableDictionary<string, int> stacks)
        {
            Stacks.Clear();
            foreach (KeyValuePair<string, int> stack in stacks)
            {
                AddItem(GameManager.StaticInstance.WorldData.GetItem(stack.Key), stack.Value);
            }
        }

        public void AddItem(ItemData item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item == item)
                {
                    while (stack.HasFreeSpace && amount > 0)
                    {
                        stack.AddToStack();
                        amount--;
                    }
                }
                if (amount == 0)
                {
                    break;
                }
            }
            while (amount > 0)
            {
                ItemStack newStack = new(item);
                Stacks.Add(newStack);
                amount--;
                while (newStack.HasFreeSpace && amount > 0)
                {
                    newStack.AddToStack();
                    amount--;
                }
            }
            UpdateInventory();
        }

        public void RemoveItem(ItemData item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            for (int i = Stacks.Count - 1; i >= 0; i--)
            {
                if (Stacks[i].Item == item)
                {
                    while (!Stacks[i].Empty && amount > 0)
                    {
                        Stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (Stacks[i].Empty)
                    {
                        Stacks.RemoveAt(i);
                    }
                }
                if (amount == 0)
                {
                    break;
                }
            }
            if (amount > 0)
            {
                Debug.LogError($"Error while removing [{item.DisplayName}] from Inventory! Left [{amount}] to remove.");
            }
            UpdateInventory();
        }

        public void DropItem(ItemData item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            // add drop mechanic
            for (int i = Stacks.Count - 1; i >= 0; i--)
            {
                if (Stacks[i].Item == item)
                {
                    while (!Stacks[i].Empty && amount > 0)
                    {
                        Stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (Stacks[i].Empty)
                    {
                        Stacks.RemoveAt(i);
                    }
                }
                if (amount == 0)
                {
                    break;
                }
            }
            if (amount > 0)
            {
                Debug.LogError($"Error while dropping [{item.DisplayName}] from Inventory! Left [{amount}] to drop.");
            }
            UpdateInventory();
        }

        public int AmountOfItem(ItemData item)
        {
            int amount = 0;
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item == item)
                {
                    amount += stack.Amount;
                }
            }
            return amount;
        }

        public void UpdateInventory()
        {
            OnInventoryChanged?.Invoke(Stacks);
        }
        // find best
        public bool GetBestWeapon(out WeaponItemData item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item.ItemType == ItemType.Weapon)
                {
                    WeaponItemData weapon = (WeaponItemData)stack.Item;
                    if (weapon.Rating > rating)
                    {
                        rating = weapon.Rating;
                        item = weapon;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestWeapon(WeaponHandType type, out WeaponItemData item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item.ItemType == ItemType.Weapon)
                {
                    WeaponItemData weapon = (WeaponItemData)stack.Item;
                    if (weapon.Rating > rating && weapon.WeaponHandType == type)
                    {
                        rating = weapon.Rating;
                        item = weapon;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestArmor(ArmorTypeData type, out ArmorItemData item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item.ItemType == ItemType.Armor)
                {
                    ArmorItemData armor = (ArmorItemData)stack.Item;
                    if (armor.Rating > rating && armor.ArmorType == type)
                    {
                        rating = armor.Rating;
                        item = armor;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestConsumable(ConsumableTypeData type, out ConsumableItemData item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in Stacks)
            {
                if (stack.Item.ItemType == ItemType.Consumable)
                {
                    ConsumableItemData consumable = (ConsumableItemData)stack.Item;
                    if (consumable.Rating > rating && consumable.ConsumableType == type)
                    {
                        rating = consumable.Rating;
                        item = consumable;
                    }
                }
            }
            return item != null;
        }
    }
}