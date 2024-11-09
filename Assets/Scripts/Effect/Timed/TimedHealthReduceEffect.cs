using UnityEngine;

namespace WinterUniverse
{
    public class TimedHealthReduceEffect : Effect
    {
        private ElementConfig _element;
        private Vector3 _hitPoint;
        private Vector3 _hitDirection;
        private float _angleHitFrom;
        private bool _playDamageAnimation;
        private bool _playDamageVFX;
        private bool _playDamageSFX;

        public ElementConfig Element => _element;
        public Vector3 HitPoint => _hitPoint;
        public Vector3 HitDirection => _hitDirection;
        public float AngleHitFrom => _angleHitFrom;
        public bool PlayDamageAnimation => _playDamageAnimation;
        public bool PlayDamageVFX => _playDamageVFX;
        public bool PlayDamageSFX => _playDamageSFX;

        public TimedHealthReduceEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public void Initialize(ElementConfig element, Vector3 hitPoint, Vector3 hitDirection, float angleFromHit, bool playDamageAnimation = true, bool playDamageVFX = true, bool playDamageSFX = true)
        {
            _element = element;
            _hitPoint = hitPoint;
            _hitDirection = hitDirection;
            _angleHitFrom = angleFromHit;
            _playDamageAnimation = playDamageAnimation;
            _playDamageVFX = playDamageVFX;
            _playDamageSFX = playDamageSFX;
        }

        public override void OnTick(float deltaTime)
        {
            if (_duration > 0f)
            {
                ProcessEffect(deltaTime);
                _duration -= deltaTime;
            }
            else
            {
                _pawn.PawnEffects.RemoveEffect(this);
            }
        }

        private void ProcessEffect(float deltaTime)
        {
            if (_pawn.IsDead)
            {
                return;
            }
            _pawn.PawnStats.ReduceCurrentHealth(_value * deltaTime, _element, _source);
        }
    }
}