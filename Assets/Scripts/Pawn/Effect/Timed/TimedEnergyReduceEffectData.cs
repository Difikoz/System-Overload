using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Energy Reduce", menuName = "Winter Universe/Effect/Timed/New Energy Reduce")]
    public class TimedEnergyReduceEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new TimedEnergyReduceEffect(this);
        }
    }

    public class TimedEnergyReduceEffect : Effect
    {
        public TimedEnergyReduceEffect(EffectData data) : base(data)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                Owner.StatModule.ReduceCurrentEnergy(Value * deltaTime);
                Duration -= deltaTime;
            }
            else
            {
                Owner.EffectModule.RemoveEffect(this);
            }
        }
    }
}