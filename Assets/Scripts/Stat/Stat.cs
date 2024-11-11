using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class Stat
    {
        private StatConfig _data;
        private float _currentValue;
        private List<float> _flatModifiers = new();
        private List<float> _multiplierModifiers = new();

        public StatConfig Data => _data;
        public float CurrentValue => _currentValue;
        public List<float> FlatModifiers => _flatModifiers;
        public List<float> MultiplierModifiers => _multiplierModifiers;

        public Stat(StatConfig data)
        {
            _data = data;
            _currentValue = 0f;// Data.BaseValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat)
            {
                _flatModifiers.Add(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier)
            {
                _multiplierModifiers.Add(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat && _flatModifiers.Contains(modifier.Value))
            {
                _flatModifiers.Remove(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier && _multiplierModifiers.Contains(modifier.Value))
            {
                _multiplierModifiers.Remove(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void CalculateCurrentValue()
        {
            float value = 0f;// _data.BaseValue;
            foreach (float f in _flatModifiers)
            {
                value += f;
            }
            float multiplierValue = 0f;
            foreach (float f in _multiplierModifiers)
            {
                multiplierValue += f;
            }
            if (multiplierValue != 0f)
            {
                multiplierValue *= value;
                multiplierValue /= 100f;
                value += multiplierValue;
            }
            if (_data.ClampMinValue && value < _data.MinValue)
            {
                value = _data.MinValue;
            }
            else if (_data.ClampMaxValue && value > _data.MaxValue)
            {
                value = _data.MaxValue;
            }
            _currentValue = value;
        }
    }
}