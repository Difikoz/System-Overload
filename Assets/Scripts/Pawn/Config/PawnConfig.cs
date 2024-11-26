using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class PawnConfig : ScriptableObject
    {
        public string CharacterName = "Name";
        public FactionConfig Faction;
        public WeaponItemConfig Weapon;
        public ArmorItemConfig Armor;
        public List<ItemStack> StartingItems = new();

        public PawnSaveData GetData()
        {
            PawnSaveData data = new()
            {
                CharacterName = CharacterName,
                Faction = Faction.DisplayName,
                Weapon = Weapon.DisplayName,
                Armor = Armor.DisplayName,
            };
            foreach (ItemStack stack in StartingItems)
            {
                if (data.InventoryStacks.ContainsKey(stack.Item.DisplayName))
                {
                    data.InventoryStacks[stack.Item.DisplayName] += stack.Amount;
                }
                else
                {
                    data.InventoryStacks.Add(stack.Item.DisplayName, stack.Amount);
                }
            }
            return data;
        }
    }
}