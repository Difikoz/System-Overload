using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "Winter Universe/Item/Consumable/New Item")]
    public class ConsumableItemData : ItemData
    {
        [Header("Consumable Information")]
        public ConsumableTypeData ConsumableType;
        public List<EffectCreator> Effects = new();

        private void OnValidate()
        {
            ItemType = ItemType.Consumable;
        }

        public override void Use(Character character, bool fromInventory = true)
        {
            foreach (EffectCreator creator in Effects)
            {
                if (creator.Chance > Random.value)
                {
                    Effect effect = creator.Effect.CreateEffect();
                    if (creator.OverrideDefaultValues)
                    {
                        effect.Value = creator.Value;
                        effect.Duration = creator.Duration;
                    }
                    character.EffectModule.AddEffect(effect);
                }
            }
            if (fromInventory)
            {
                character.InventoryModule.RemoveItem(this);
            }
        }
    }
}