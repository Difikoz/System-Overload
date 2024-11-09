using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatRequirement
    {
        [SerializeField] private StatConfig _stat;
        [SerializeField] private RequirementType _type;
        [SerializeField] private float _value;

        public StatConfig Stat => _stat;
        public RequirementType Type => _type;
        public float Value => _value;

        public StatRequirement(StatConfig stat, RequirementType type, float value)
        {
            _stat = stat;
            _type = type;
            _value = value;
        }
    }
}