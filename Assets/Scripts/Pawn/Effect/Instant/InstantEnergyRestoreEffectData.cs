using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Energy Restore", menuName = "Winter Universe/Effect/Instant/New Energy Restore")]
    public class InstantEnergyRestoreEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new InstantEnergyRestoreEffect(this);
        }
    }

    public class InstantEnergyRestoreEffect : Effect
    {
        public InstantEnergyRestoreEffect(EffectData data) : base(data)
        {
        }

        public override void OnApply()
        {
            Owner.PawnStats.RestoreCurrentEnergy(Value);
            Owner.PawnEffects.RemoveEffect(this);
        }
    }
}