using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ItemData : ScriptableObject
    {
        [Header("Basic Information")]
        public string DisplayName = "Item";
        [TextArea] public string Description = "Description";
        public Sprite Icon;
        public ItemType ItemType;
        public GameObject Model;
        public float Weight = 1f;
        public int MaxCountInStack = 1;
        public int Price = 100;
        public float Rating = 1f;
        [Header("Requirements for usable items")]
        public int RequiredLevel;
        public List<RaceData> RequiredRace = new();
        public List<FactionData> RequiredFaction = new();
        public List<StatRequirement> RequiredStats = new();

        public virtual bool CanUse(Character character, out string error, bool fromInventory = true)
        {
            error = string.Empty;
            if (character.StatModule.Level < RequiredLevel)
            {
                error = "Low Level";
                return false;
            }
            if (RequiredRace.Count > 0)
            {
                if (!RequiredRace.Contains(character.Race))
                {
                    error = "Wrong Race";
                    return false;
                }
            }
            if (RequiredFaction.Count > 0)
            {
                if (!RequiredFaction.Contains(character.Faction))
                {
                    error = "Wrong Faction";
                    return false;
                }
            }
            if (RequiredStats.Count > 0)
            {
                foreach (StatRequirement requirement in RequiredStats)
                {
                    if (requirement.Type == RequirementType.GreaterOrEqual)
                    {
                        if (character.StatModule.GetStatByName(requirement.Stat.DisplayName).CurrentValue < requirement.Value)
                        {
                            error = $"Low {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                    else if (requirement.Type == RequirementType.LessOrEqual)
                    {
                        if (character.StatModule.GetStatByName(requirement.Stat.DisplayName).CurrentValue > requirement.Value)
                        {
                            error = $"High {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                    else if (requirement.Type == RequirementType.Equal)
                    {
                        if (character.StatModule.GetStatByName(requirement.Stat.DisplayName).CurrentValue != requirement.Value)
                        {
                            error = $"Not equal {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                }
            }
            return !fromInventory || character.InventoryModule.AmountOfItem(this) > 0;
        }

        public virtual void Use(Character character, bool fromInventory = true)
        {

        }
    }

    [System.Serializable]
    public class ItemStack
    {
        public ItemData Item;
        public int Amount;

        public bool HasFreeSpace => Amount < Item.MaxCountInStack;
        public bool Empty => Amount <= 0;

        public void AddToStack(int value = 1)
        {
            Amount += value;
        }

        public void RemoveFromStack(int value = 1)
        {
            Amount -= value;
        }

        public ItemStack(ItemData item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }
    }
}