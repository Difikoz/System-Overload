using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Energy Reduce", menuName = "Winter Universe/Effect/Instant/New Energy Reduce")]
    public class InstantEnergyReduceEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new InstantEnergyReduceEffect(this);
        }
    }

    public class InstantEnergyReduceEffect : Effect
    {
        public InstantEnergyReduceEffect(EffectData data) : base(data)
        {
        }

        public override void OnApply()
        {
            Owner.StatModule.ReduceCurrentEnergy(Value);
            Owner.EffectModule.RemoveEffect(this);
        }
    }
}