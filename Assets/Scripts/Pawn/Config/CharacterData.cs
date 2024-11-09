using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Character", menuName = "Winter Universe/Character/New Character")]
    public class CharacterData : ScriptableObject
    {
        public string CharacterName = "Name";
        public int StartingLevel = 1;
        public RaceConfig Race;
        public Gender Gender;
        public FactionConfig Faction;
        public List<ItemStack> StartingItems = new();

        public CharacterSaveData GetData()
        {
            CharacterSaveData data = new()
            {
                CharacterName = CharacterName,
                Level = StartingLevel,
                Race = Race.DisplayName,
                Gender = Gender.ToString(),
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