using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Character", menuName = "Winter Universe/Character/New Character")]
    public class CharacterData : ScriptableObject
    {
        public string CharacterName = "Name";
        public FactionConfig Faction;
        public List<ItemStack> StartingItems = new();

        public CharacterSaveData GetData()
        {
            CharacterSaveData data = new()
            {
                CharacterName = CharacterName,
                Faction = Faction.DisplayName
            };
            foreach (ItemStack stack in StartingItems)
            {
                data.InventoryStacks.Add(stack.Item.DisplayName, stack.Amount);
            }
            return data;
        }
    }
}