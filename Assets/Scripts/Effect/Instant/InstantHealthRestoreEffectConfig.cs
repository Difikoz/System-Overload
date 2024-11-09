using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Health Restore", menuName = "Winter Universe/Effect/Instant/New Health Restore")]
    public class InstantHealthRestoreEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new InstantHealthRestoreEffect(this, pawn, source, value, duration);
        }
    }
}