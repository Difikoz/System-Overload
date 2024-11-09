using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Health Restore", menuName = "Winter Universe/Effect/Instant/New Health Restore")]
    public class InstantHealthRestoreEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new InstantHealthRestoreEffect(this);
        }
    }

    public class InstantHealthRestoreEffect : Effect
    {
        public InstantHealthRestoreEffect(EffectData data) : base(data)
        {
        }

        public override void OnApply()
        {
            Owner.StatModule.RestoreCurrentHealth(Value);
            Owner.EffectModule.RemoveEffect(this);
        }
    }
}