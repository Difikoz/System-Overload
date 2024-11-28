using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        public Action<List<ItemStack>> OnInventoryChanged;

        private List<ItemStack> _stacks = new();

        public List<ItemStack> Stacks => _stacks;

        public virtual void Initialize(SerializableDictionary<string, int> stacks)
        {
            _stacks.Clear();
            foreach (KeyValuePair<string, int> stack in stacks)
            {
                AddItem(GameManager.StaticInstance.ConfigsManager.GetItem(stack.Key), stack.Value);
            }
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            foreach (ItemStack stack in _stacks)
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
                _stacks.Add(newStack);
                amount--;
                while (newStack.HasFreeSpace && amount > 0)
                {
                    newStack.AddToStack();
                    amount--;
                }
            }
            UpdateInventory();
        }

        public void RemoveItem(ItemConfig item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item == item)
                {
                    while (!_stacks[i].Empty && amount > 0)
                    {
                        _stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (_stacks[i].Empty)
                    {
                        _stacks.RemoveAt(i);
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

        public void DropItem(ItemConfig item, int amount = 1)
        {
            if (item == null)
            {
                return;
            }
            // add drop mechanic
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item == item)
                {
                    while (!_stacks[i].Empty && amount > 0)
                    {
                        _stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (_stacks[i].Empty)
                    {
                        _stacks.RemoveAt(i);
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

        public int AmountOfItem(ItemConfig item)
        {
            int amount = 0;
            foreach (ItemStack stack in _stacks)
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
            OnInventoryChanged?.Invoke(_stacks);
        }
        // find best
        public bool GetBestWeapon(out WeaponItemConfig item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType.DisplayName == "Weapon")
                {
                    WeaponItemConfig weapon = (WeaponItemConfig)stack.Item;
                    if (weapon.Rating > rating)
                    {
                        rating = weapon.Rating;
                        item = weapon;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestArmor(out ArmorItemConfig item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType.DisplayName == "Armor")
                {
                    ArmorItemConfig armor = (ArmorItemConfig)stack.Item;
                    if (armor.Rating > rating)
                    {
                        rating = armor.Rating;
                        item = armor;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestConsumable(ConsumableTypeConfig type, out ConsumableItemConfig item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType.DisplayName == "Consumable")
                {
                    ConsumableItemConfig consumable = (ConsumableItemConfig)stack.Item;
                    if (consumable.Rating > rating && consumable.ConsumableType == type)
                    {
                        rating = consumable.Rating;
                        item = consumable;
                    }
                }
            }
            return item != null;
        }

        public bool GetBestConsumable(string type, out ConsumableItemConfig item)
        {
            item = null;
            float rating = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType.DisplayName == "Consumable")
                {
                    ConsumableItemConfig consumable = (ConsumableItemConfig)stack.Item;
                    if (consumable.Rating > rating && consumable.ConsumableType.DisplayName == type)
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