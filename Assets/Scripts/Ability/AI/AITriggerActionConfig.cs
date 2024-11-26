using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Trigger Action", menuName = "Winter Universe/Action/AI/New Trigger")]
    public class AITriggerActionConfig : AIActionConfig
    {
        public string AnimName = "Dodge Backward";

        public override void AttempToPerformAction(AIController ai)
        {
            ai.Pawn.PawnAnimator.PlayActionAnimation(AnimName, true);
        }
    }
}