using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Type", menuName = "Winter Universe/Item/Armor/New Type")]
    public class ArmorTypeConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}