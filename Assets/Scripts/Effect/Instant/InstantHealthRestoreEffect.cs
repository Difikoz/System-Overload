using UnityEngine;

namespace WinterUniverse
{
    public class InstantHealthRestoreEffect : Effect
    {
        public InstantHealthRestoreEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _pawn.PawnStats.RestoreCurrentHealth(_value);
            _pawn.PawnEffects.RemoveEffect(this);
        }
    }
}