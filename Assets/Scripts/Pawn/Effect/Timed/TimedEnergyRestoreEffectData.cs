using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Energy Restore", menuName = "Winter Universe/Effect/Timed/New Energy Restore")]
    public class TimedEnergyRestoreEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new TimedEnergyRestoreEffect(this);
        }
    }

    public class TimedEnergyRestoreEffect : Effect
    {
        public TimedEnergyRestoreEffect(EffectData data) : base(data)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                Owner.PawnStats.RestoreCurrentEnergy(Value * deltaTime);
                Duration -= deltaTime;
            }
            else
            {
                Owner.PawnEffects.RemoveEffect(this);
            }
        }
    }
}