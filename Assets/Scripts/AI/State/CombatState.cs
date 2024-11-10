using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Combat State", menuName = "Winter Universe/Character/NPC/State/New Combat State")]
    public class CombatState : NPCState
    {
        public float MaxCombatRadius = 10f;

        public List<NPCActionData> Actions = new();

        protected List<NPCActionData> _potentialActions = new();
        protected NPCActionData _currentAction;
        protected NPCActionData _lastAction;
        protected bool _hasAction;

        public CombatState()
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i] = Instantiate(Actions[i]);
            }
        }

        public override NPCState Tick(AIController npc)
        {
            if (npc.IsPerfomingAction)
            {
                return this;
            }
            if (npc.PawnCombat.CurrentTarget == null || npc.PawnCombat.CurrentTarget.IsDead)
            {
                return SwitchState(npc, npc.IdleState);
            }
            if (npc.PawnCombat.DistanceToTarget > MaxCombatRadius)
            {
                return SwitchState(npc, npc.ChaseState);
            }
            if (_hasAction)
            {
                npc.AttackState.Action = _currentAction;
                return SwitchState(npc, npc.AttackState);
            }
            else
            {
                GetNewAction(npc);
            }
            npc.Agent.SetDestination(npc.PawnCombat.CurrentTarget.transform.position);
            return this;
        }

        protected virtual void GetNewAction(AIController npc)
        {
            _potentialActions.Clear();
            foreach (NPCActionData potential in Actions)
            {
                if (!potential.InRangeToUse(npc))
                {
                    continue;
                }
                if (!potential.InAngleToUse(npc))
                {
                    continue;
                }
                _potentialActions.Add(potential);
            }
            if (_potentialActions.Count <= 0)
            {
                return;
            }
            float totalWeight = 0;
            foreach (NPCActionData potential in _potentialActions)
            {
                totalWeight += potential.Weight;
            }
            float randomWeight = Random.Range(0f, totalWeight);
            float currentWeight = 0f;
            foreach (NPCActionData potential in _potentialActions)
            {
                currentWeight += potential.Weight;
                if (randomWeight <= currentWeight)
                {
                    _currentAction = potential;
                    _lastAction = _currentAction;
                    _hasAction = true;
                    return;
                }
            }
        }

        protected virtual bool RollForOutcomeChance(float outcomeChance)
        {
            return Random.value < outcomeChance;
        }

        protected override void ResetFlags(AIController npc)
        {
            base.ResetFlags(npc);
            _hasAction = false;
        }
    }
}