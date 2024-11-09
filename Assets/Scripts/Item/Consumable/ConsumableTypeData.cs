using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Type", menuName = "Winter Universe/Item/Consumable/New Type")]
    public class ConsumableTypeData : ScriptableObject
    {
        [SerializeField] private string _displayName = "Type";
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}