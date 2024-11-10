using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Item", menuName = "Winter Universe/Item/Equipment/Armor/New Item")]
    public class ArmorItemConfig : ItemConfig
    {
        [Header("Equipment Modifiers")]
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [Header("Armor Information")]
        [SerializeField] private ArmorTypeConfig _armorType;

        public List<StatModifierCreator> Modifiers => _modifiers;
        public ArmorTypeConfig ArmorType => _armorType;

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