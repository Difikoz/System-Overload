using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Type", menuName = "Winter Universe/Item/Consumable/New Type")]
    public class ConsumableTypeData : ScriptableObject
    {
        public string DisplayName = "Type";
        public Sprite Icon;
    }
}