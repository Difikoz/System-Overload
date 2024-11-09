using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Health Restore", menuName = "Winter Universe/Effect/Timed/New Health Restore")]
    public class TimedHealthRestoreEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new TimedHealthRestoreEffect(this, pawn, source, value, duration);
        }
    }
}