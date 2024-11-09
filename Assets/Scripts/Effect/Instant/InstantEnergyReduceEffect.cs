using UnityEngine;

namespace WinterUniverse
{
    public class InstantEnergyReduceEffect : Effect
    {
        public InstantEnergyReduceEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _pawn.PawnStats.ReduceCurrentEnergy(_value);
            _pawn.PawnEffects.RemoveEffect(this);
        }
    }
}