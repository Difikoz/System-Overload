using UnityEngine;

namespace WinterUniverse
{
    public abstract class AIActionConfig : ScriptableObject
    {
        [Range(0f, 1f)] public float Weight = 0.5f;
        public float Cooldown = 1.5f;
        public float Angle = 45f;
        public float MinDistance = 2f;
        public float MaxDistance = 4f;
        public AIActionConfig ComboAction;
        [Range(0f, 1f)] public float ComboChance = 0.25f;

        public abstract void AttempToPerformAction(AIController ai);

        public virtual bool InRangeToUse(AIController ai)
        {
            return ai.Pawn.PawnCombat.DistanceToTarget > MinDistance && ai.Pawn.PawnCombat.DistanceToTarget < MaxDistance;
        }

        public virtual bool InAngleToUse(AIController ai)
        {
            return Mathf.Abs(ai.Pawn.PawnCombat.AngleToTarget) < Angle / 2f;
        }
    }
}