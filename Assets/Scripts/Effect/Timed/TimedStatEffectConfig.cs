using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Stat", menuName = "Winter Universe/Effect/Timed/New Stat")]
    public class TimedStatEffectConfig : EffectConfig
    {
        [SerializeField] private StatModifierCreator _modifier;

        public StatModifierCreator Modifier => _modifier;

        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new TimedStatEffect(this, pawn, source, value, duration);
        }
    }
}