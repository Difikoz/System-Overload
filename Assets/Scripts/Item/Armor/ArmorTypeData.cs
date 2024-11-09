using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Type", menuName = "Winter Universe/Item/Equipment/Armor/New Type")]
    public class ArmorTypeData : ScriptableObject
    {
        public string DisplayName = "Type";
        public Sprite Icon;
    }
}