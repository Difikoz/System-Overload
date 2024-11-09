using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Energy Reduce", menuName = "Winter Universe/Effect/Instant/New Energy Reduce")]
    public class InstantEnergyReduceEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new InstantEnergyReduceEffect(this, pawn, source, value, duration);
        }
    }
}