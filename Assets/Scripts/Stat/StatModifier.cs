using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatModifier
    {
        [SerializeField] private StatModifierType _type;
        [SerializeField] private float _value;

        public StatModifierType Type => _type;
        public float Value => _value;

        public StatModifier(StatModifierType type, float value)
        {
            _type = type;
            _value = value;
        }
    }
}