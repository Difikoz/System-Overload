using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "Winter Universe/Item/Consumable/New Item")]
    public class ConsumableItemConfig : ItemConfig
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

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            foreach (EffectCreator creator in _effects)
            {
                if (creator.Chance > Random.value)
                {
                    if (creator.OverrideDefaultValues)
                    {
                        pawn.PawnEffects.AddEffect(creator.Effect.CreateEffect(pawn, null, creator.Value, creator.Duration));
                    }
                    else
                    {
                        pawn.PawnEffects.AddEffect(creator.Effect.CreateEffect(pawn, null, creator.Effect.Value, creator.Effect.Duration));
                    }
                }
            }
            if (fromInventory)
            {
                pawn.PawnInventory.RemoveItem(this);
            }
        }
    }
}