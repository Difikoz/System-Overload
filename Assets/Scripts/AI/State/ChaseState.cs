using UnityEngine;
//using UnityEngine.AI;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Chase State", menuName = "Winter Universe/Character/NPC/State/New Chase State")]
    public class ChaseState : NPCState
    {
        public override NPCState Tick(AIController npc)
        {
            if (npc.Pawn.IsPerfomingAction)
            {
                return this;
            }
            if (npc.Pawn.PawnCombat.CurrentTarget == null || npc.Pawn.PawnCombat.CurrentTarget.IsDead)
            {
                return SwitchState(npc, npc.IdleState);
            }
            if (npc.Pawn.PawnCombat.DistanceToTarget <= npc.CombatPhase.CurrentPhase.State.MaxCombatRadius)
            {
                return SwitchState(npc, npc.CombatPhase.CurrentPhase.State);
            }
            npc.Agent.SetDestination(npc.Pawn.PawnCombat.CurrentTarget.transform.position);
            //NavMeshPath path = new();
            //npc.Agent.CalculatePath(npc.NPCCombatManager.CurrentTarget.transform.position, path);
            //npc.Agent.SetPath(path);
            return this;
        }
    }
}