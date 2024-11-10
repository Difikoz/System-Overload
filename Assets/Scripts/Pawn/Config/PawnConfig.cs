using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class PawnConfig : ScriptableObject
    {
        public string CharacterName = "Name";
        public FactionConfig Faction;
        public WeaponItemConfig WeaponInRightHand;
        public WeaponItemConfig WeaponInLeftHand;
        public List<ItemStack> StartingItems = new();

        public PawnSaveData GetData()
        {
            PawnSaveData data = new()
            {
                CharacterName = CharacterName,
                Faction = Faction.DisplayName,
                WeaponInRightHand = WeaponInRightHand.DisplayName,
                WeaponInLeftHand = WeaponInLeftHand.DisplayName,
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