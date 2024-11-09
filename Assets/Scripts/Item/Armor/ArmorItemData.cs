using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Item", menuName = "Winter Universe/Item/Equipment/Armor/New Item")]
    public class ArmorItemData : ItemData
    {
        [Header("Equipment Modifiers")]
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [Header("Armor Information")]
        [SerializeField] private ArmorTypeData _armorType;

        public List<StatModifierCreator> Modifiers => _modifiers;
        public ArmorTypeData ArmorType => _armorType;

        private void OnValidate()
        {
            _itemType = ItemType.Armor;
        }

        public override void Use(PawnController character, bool fromInventory = true)
        {
            character.PawnEquipment.EquipArmor(this, fromInventory);
        }
    }
}