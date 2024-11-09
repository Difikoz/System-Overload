using Unity.VisualScripting;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Stat", menuName = "Winter Universe/Effect/Timed/New Stat")]
    public class TimedStatEffectData : EffectData
    {
        public StatModifierCreator Modifier;

        public override Effect CreateEffect()
        {
            return new TimedStatEffect(this);
        }
    }

    public class TimedStatEffect : Effect
    {
        public StatModifierCreator Modifier;

        public TimedStatEffect(TimedStatEffectData data) : base(data)
        {
            Modifier ??= data.Modifier;
        }

        public override void OnApply()
        {
            Owner.StatModule.AddStatModifier(Modifier);
        }

        public override void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                Duration -= deltaTime;
            }
            else
            {
                Owner.EffectModule.RemoveEffect(this);
            }
        }

        public override void OnRemove()
        {
            Owner.StatModule.RemoveStatModifier(Modifier);
        }
    }
}