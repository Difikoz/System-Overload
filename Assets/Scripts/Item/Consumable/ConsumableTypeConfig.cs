using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable Type", menuName = "Winter Universe/Item/Consumable/New Type")]
    public class ConsumableTypeConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}