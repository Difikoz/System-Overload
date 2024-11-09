using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Health Reduce", menuName = "Winter Universe/Effect/Timed/New Health Reduce")]
    public class TimedHealthReduceEffectConfig : EffectConfig
    {
        [SerializeField] private ElementConfig _element;
        [SerializeField] private bool _playDamageAnimation;
        [SerializeField] private bool _playDamageVFX;
        [SerializeField] private bool _playDamageSFX;

        public ElementConfig Element => _element;
        public bool PlayDamageAnimation => _playDamageAnimation;
        public bool PlayDamageVFX => _playDamageVFX;
        public bool PlayDamageSFX => _playDamageSFX;

        public override Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration)
        {
            return new TimedHealthReduceEffect(this, pawn, source, value, duration);
        }
    }
}