using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Winter Universe/Stat/New Stat")]
    public class StatData : ScriptableObject
    {
        public string DisplayName = "Stat";
        [TextArea] public string Description;
        public Sprite Icon;
        public float BaseValue;
        public bool ClampMinValue;
        public float MinValue;
        public bool ClampMaxValue;
        public float MaxValue;
        public bool IsPercent;
    }

    [System.Serializable]
    public class StatModifier
    {
        public StatModifierType Type;
        public float Value;
    }

    [System.Serializable]
    public class StatModifierCreator
    {
        public StatData Stat;
        public StatModifier Modifier;
    }

    [System.Serializable]
    public class StatValue
    {
        public StatData Stat;
        public float Value;
    }

    [System.Serializable]
    public class StatRequirement
    {
        public StatData Stat;
        public RequirementType Type;
        public float Value;
    }

    [System.Serializable]
    public class Stat
    {
        public StatData Data;
        public float CurrentValue;
        public List<float> FlatModifiers = new();
        public List<float> MultiplierModifiers = new();

        public Stat(StatData data)
        {
            Data = data;
            CurrentValue = Data.BaseValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat)
            {
                FlatModifiers.Add(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier)
            {
                MultiplierModifiers.Add(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat && FlatModifiers.Contains(modifier.Value))
            {
                FlatModifiers.Remove(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier && MultiplierModifiers.Contains(modifier.Value))
            {
                MultiplierModifiers.Remove(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void CalculateCurrentValue()
        {
            float value = Data.BaseValue;
            foreach (float f in FlatModifiers)
            {
                value += f;
            }
            float multiplierValue = 0f;
            foreach (float f in MultiplierModifiers)
            {
                multiplierValue += f;
            }
            if (multiplierValue != 0f)
            {
                multiplierValue *= value;
                multiplierValue /= 100f;
                value += multiplierValue;
            }
            if (Data.ClampMinValue && value < Data.MinValue)
            {
                value = Data.MinValue;
            }
            else if (Data.ClampMaxValue && value > Data.MaxValue)
            {
                value = Data.MaxValue;
            }
            CurrentValue = value;
        }
    }
}