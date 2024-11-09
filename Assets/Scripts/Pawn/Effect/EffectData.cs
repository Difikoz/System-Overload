using UnityEngine;

namespace WinterUniverse
{
    public abstract class EffectData : ScriptableObject
    {
        [Header("Rework Effects to be Gameobjects\nNeed for timed effects\nDelay, Time Rate etc")]
        public string DisplayName = "Effect";
        public Sprite Icon;
        public float Value = 1;
        public float Duration = 0f;
        public bool IsPositive;

        public abstract Effect CreateEffect();
    }

    [System.Serializable]
    public class EffectCreator
    {
        public EffectData Effect;
        public bool OverrideDefaultValues;
        public float Value;
        public float Duration;
        [Range(0f, 1f)] public float Chance = 0.5f;
    }

    public abstract class Effect
    {
        public Character Owner;
        public Character Source;

        public EffectData Data;

        public float Value;
        public float Duration;

        public Effect(EffectData data)
        {
            Data = data;
            Value = Data.Value;
            Duration = Data.Duration;
        }

        public virtual void OnApply()
        {

        }

        public virtual void OnTick(float deltaTime)
        {

        }

        public virtual void OnRemove()
        {

        }
    }
}