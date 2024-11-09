using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Health Reduce", menuName = "Winter Universe/Effect/Instant/New Health Reduce")]
    public class InstantHealthReduceEffectConfig : EffectConfig
    {
        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new InstantHealthReduceEffect(this, pawn, source, value, duration);
        }
    }
}