using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Winter Universe/Character/NPC/State/New Idle State")]
    public class IdleState : NPCState
    {
        public bool CanChase = true;

        public override NPCState Tick(NPCController npc)
        {
            if (CanChase)
            {
                if (npc.CombatModule.CurrentTarget != null)
                {
                    return SwitchState(npc, npc.ChaseState);
                }
                else
                {
                    npc.NPCDetectionModule.FindTargetInViewRange();
                }
            }
            return this;
        }
    }
}