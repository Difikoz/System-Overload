using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class EffectCreator
    {
        [SerializeField] private EffectConfig _effect;
        [SerializeField, Range(0f, 1f)] private float _chance = 0.5f;
        [SerializeField] private bool _overrideDefaultValues;
        [SerializeField] private float _value;
        [SerializeField] private float _duration;

        public EffectConfig Effect => _effect;
        public float Chance => _chance;
        public bool OverrideDefaultValues => _overrideDefaultValues;
        public float Value => _value;
        public float Duration => _duration;
    }
}