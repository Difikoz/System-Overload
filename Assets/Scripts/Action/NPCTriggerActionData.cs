using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Trigger Action", menuName = "Winter Universe/Character/NPC/Action/New Trigger")]
    public class NPCTriggerActionData : NPCActionData
    {
        public string AnimName = "Dodge Backward";

        public override void AttempToPerformAction(NPCController npc)
        {
            npc.PawnAnimator.PlayActionAnimation(AnimName, true);
        }
    }
}