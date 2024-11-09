using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Energy Reduce", menuName = "Winter Universe/Effect/Timed/New Energy Reduce")]
    public class TimedEnergyReduceEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new TimedEnergyReduceEffect(this, pawn, source, value, duration);
        }
    }
}