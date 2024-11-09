using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class DamageType
    {
        [SerializeField] private float _damage;
        [SerializeField] private ElementData _element;

        public float Damage => _damage;
        public ElementData Element => _element;
    }
}