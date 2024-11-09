using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Energy Restore", menuName = "Winter Universe/Effect/Instant/New Energy Restore")]
    public class InstantEnergyRestoreEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new InstantEnergyRestoreEffect(this, pawn, source, value, duration);
        }
    }
}