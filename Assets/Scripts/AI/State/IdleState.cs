using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Winter Universe/Character/NPC/State/New Idle State")]
    public class IdleState : NPCState
    {
        public bool CanChase = true;

        public override NPCState Tick(AIController npc)
        {
            if (CanChase)
            {
                if (npc.Pawn.PawnCombat.CurrentTarget != null)
                {
                    return SwitchState(npc, npc.ChaseState);
                }
                else
                {
                    npc.AIDetectionModule.FindTargetInViewRange();
                }
            }
            return this;
        }
    }
}