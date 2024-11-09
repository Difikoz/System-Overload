using UnityEngine;

namespace WinterUniverse
{
    public class TimedStatEffect : Effect
    {
        private StatModifierCreator _modifier;

        public StatModifierCreator Modifier => _modifier;

        public TimedStatEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public void Initialize(StatModifierCreator modifier)
        {
            _modifier = modifier;
        }

        public override void OnApply()
        {
            _pawn.PawnStats.AddStatModifier(_modifier);
        }

        public override void OnTick(float deltaTime)
        {
            if (_duration > 0f)
            {
                _duration -= deltaTime;
            }
            else
            {
                _pawn.PawnEffects.RemoveEffect(this);
            }
        }

        public override void OnRemove()
        {
            _pawn.PawnStats.RemoveStatModifier(_modifier);
        }
    }
}