using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Item", menuName = "Winter Universe/Item/Equipment/Armor/New Item")]
    public class ArmorItemData : ItemData
    {
        [Header("Equipment Modifiers")]
        public List<StatModifierCreator> Modifiers = new();
        [Header("Armor Information")]
        public ArmorTypeData ArmorType;

        private void OnValidate()
        {
            ItemType = ItemType.Armor;
        }

        public override void Use(PawnController character, bool fromInventory = true)
        {
            character.EquipmentModule.EquipArmor(this, fromInventory);
        }
    }
}