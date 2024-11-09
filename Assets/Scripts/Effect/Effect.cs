using UnityEngine;

namespace WinterUniverse
{
    public abstract class Effect
    {
        protected EffectConfig _config;
        protected PawnController _pawn;
        protected PawnController _source;
        protected float _value;
        protected float _duration;

        public EffectConfig Config => _config;
        public PawnController Pawn => _pawn;
        public PawnController Source => _source;
        public float Value => _value;
        public float Duration => _duration;

        public Effect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration)
        {
            _config = config;
            _pawn = pawn;
            _source = source;
            _value = value;
            _duration = duration;
        }

        public virtual void OnApply()
        {

        }

        public virtual void OnTick(float deltaTime)
        {

        }

        public virtual void OnRemove()
        {

        }
    }
}