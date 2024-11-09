using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Energy Restore", menuName = "Winter Universe/Effect/Timed/New Energy Restore")]
    public class TimedEnergyRestoreEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new TimedEnergyRestoreEffect(this, pawn, source, value, duration);
        }
    }
}