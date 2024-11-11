using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon Type", menuName = "Winter Universe/Item/Weapon/New Type")]
    public class WeaponTypeConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}