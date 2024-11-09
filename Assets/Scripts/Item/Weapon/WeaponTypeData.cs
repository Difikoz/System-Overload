using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon Type", menuName = "Winter Universe/Item/Equipment/Weapon/New Type")]
    public class WeaponTypeData : ScriptableObject
    {
        public string DisplayName = "Type";
        public Sprite Icon;
    }
}