using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Health Restore", menuName = "Winter Universe/Effect/Timed/New Health Restore")]
    public class TimedHealthRestoreEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new TimedHealthRestoreEffect(this);
        }
    }

    public class TimedHealthRestoreEffect : Effect
    {
        public TimedHealthRestoreEffect(EffectData data) : base(data)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                Owner.StatModule.RestoreCurrentHealth(Value * deltaTime);
                Duration -= deltaTime;
            }
            else
            {
                Owner.EffectModule.RemoveEffect(this);
            }
        }
    }
}