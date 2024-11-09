using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class DamageType
    {
        [SerializeField] private float _damage;
        [SerializeField] private ElementConfig _element;

        public float Damage => _damage;
        public ElementConfig Element => _element;
    }
}