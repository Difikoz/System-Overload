using UnityEngine;

namespace WinterUniverse
{
    public class TimedEnergyRestoreEffect : Effect
    {
        public TimedEnergyRestoreEffect(EffectConfig config, PawnController pawn, PawnController source, float value, float duration) : base(config, pawn, source, value, duration)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (_duration > 0f)
            {
                _pawn.PawnStats.RestoreCurrentEnergy(_value * deltaTime);
                _duration -= deltaTime;
            }
            else
            {
                _pawn.PawnEffects.RemoveEffect(this);
            }
        }
    }
}