using UnityEngine;

namespace WinterUniverse
{
    public class TimedHealthRestoreEffect : Effect
    {
        public TimedHealthRestoreEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (_duration > 0f)
            {
                _pawn.PawnStats.RestoreCurrentHealth(_value * deltaTime);
                _duration -= deltaTime;
            }
            else
            {
                _pawn.PawnEffects.RemoveEffect(this);
            }
        }
    }
}