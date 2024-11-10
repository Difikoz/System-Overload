using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class InstantHealthReduceEffect : Effect
    {
        private ElementConfig _element;
        private Vector3 _hitPoint;
        private Vector3 _hitDirection;
        private bool _playDamageVFX;
        private bool _playDamageSFX;

        public ElementConfig Element => _element;
        public Vector3 HitPoint => _hitPoint;
        public Vector3 HitDirection => _hitDirection;
        public bool PlayDamageVFX => _playDamageVFX;
        public bool PlayDamageSFX => _playDamageSFX;

        public InstantHealthReduceEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public void Initialize(ElementConfig element, Vector3 hitPoint, Vector3 hitDirection, bool playDamageVFX = true, bool playDamageSFX = true)
        {
            _element = element;
            _hitPoint = hitPoint;
            _hitDirection = hitDirection;
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
            _pawn.PawnStats.ReduceCurrentHealth(Value < 1f ? 1f : _value, _element, _source);
        }
    }
}