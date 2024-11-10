using UnityEngine;

namespace WinterUniverse
{
    public class NPCActionData : ScriptableObject
    {
        [Range(0f, 1f)] public float Weight = 0.5f;
        public float Cooldown = 1.5f;
        public float Angle = 45f;
        public float MinDistance = 2f;
        public float MaxDistance = 4f;
        public NPCActionData ComboAction;
        [Range(0f, 1f)] public float ComboChance = 0.25f;

        public virtual void AttempToPerformAction(AIController npc)
        {

        }

        public virtual bool InRangeToUse(AIController npc)
        {
            return npc.PawnCombat.DistanceToTarget > MinDistance && npc.PawnCombat.DistanceToTarget < MaxDistance;
        }

        public virtual bool InAngleToUse(AIController npc)
        {
            return Mathf.Abs(npc.PawnCombat.AngleToTarget) < Angle / 2f;
        }
    }
}