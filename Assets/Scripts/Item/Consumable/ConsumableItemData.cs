using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "Winter Universe/Item/Consumable/New Item")]
    public class ConsumableItemData : ItemData
    {
        [Header("Consumable Information")]
        [SerializeField] private ConsumableTypeData _consumableType;
        [SerializeField] private List<EffectCreator> _effects = new();

        public ConsumableTypeData ConsumableType => _consumableType;
        public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Consumable;
        }

        public override void Use(PawnController character, bool fromInventory = true)
        {
            foreach (EffectCreator creator in _effects)
            {
                if (creator.Chance > Random.value)
                {
                    Effect effect = creator.Effect.CreateEffect();
                    if (creator.OverrideDefaultValues)
                    {
                        effect.Value = creator.Value;
                        effect.Duration = creator.Duration;
                    }
                    character.PawnEffects.AddEffect(effect);
                }
            }
            if (fromInventory)
            {
                character.PawnInventory.RemoveItem(this);
            }
        }
    }
}