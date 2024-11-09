using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class InstantHealthReduceEffect : Effect
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

        public InstantHealthReduceEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
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

        public override void OnApply()
        {
            ProcessEffect();
            _pawn.PawnEffects.RemoveEffect(this);
        }

        private void ProcessEffect()
        {
            if (_pawn.IsDead)
            {
                return;
            }
            if (_playDamageSFX)
            {
                if (_element.HitClips.Count > 0)
                {
                    _pawn.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.ChooseRandomClip(_element.HitClips));
                }
                _pawn.PawnSound.PlayGetHitClip();
            }
            if (_playDamageVFX)
            {
                if (_element.HitVFX.Count > 0)
                {
                    LeanPool.Spawn(_element.HitVFX[Random.Range(0, _element.HitVFX.Count)], _hitPoint, _hitDirection != Vector3.zero ? Quaternion.LookRotation(_hitDirection) : Quaternion.identity);
                }
                _pawn.PawnEffects.SpawnBloodSplatterVFX(_hitPoint, _hitDirection);
            }
            if (_playDamageAnimation)
            {
                PlayDirectionBasedDamageAnimation();
            }
            _pawn.PawnStats.ReduceCurrentHealth(Value < 1f ? 1f : _value, _element, _source);
        }

        private void PlayDirectionBasedDamageAnimation()
        {
            if ((_angleHitFrom >= 135f && _angleHitFrom <= 180f) || (_angleHitFrom <= -135f && _angleHitFrom >= -180f))//front
            {
                _pawn.PawnAnimator.PlayActionAnimation("Get Hit Forward", true);
            }
            else if (_angleHitFrom >= -45f && _angleHitFrom <= 45f)//back
            {
                _pawn.PawnAnimator.PlayActionAnimation("Get Hit Backward", true);
            }
            else if (_angleHitFrom >= 45f && _angleHitFrom <= 135f)//right
            {
                _pawn.PawnAnimator.PlayActionAnimation("Get Hit Right", true);
            }
            else if (_angleHitFrom >= -135f && _angleHitFrom <= -45f)//left
            {
                _pawn.PawnAnimator.PlayActionAnimation("Get Hit Left", true);
            }
            else
            {
                _pawn.PawnAnimator.PlayActionAnimation("Get Hit Forward", true);
            }
        }
    }
}