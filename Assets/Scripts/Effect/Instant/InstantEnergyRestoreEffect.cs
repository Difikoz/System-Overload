using UnityEngine;

namespace WinterUniverse
{
    public class InstantEnergyRestoreEffect : Effect
    {
        public InstantEnergyRestoreEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _pawn.PawnStats.RestoreCurrentEnergy(_value);
            _pawn.PawnEffects.RemoveEffect(this);
        }
    }
}