using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Attack State", menuName = "Winter Universe/Character/NPC/State/New Attack State")]
    public class AttackState : NPCState
    {
        [HideInInspector] public NPCActionData Action;

        protected bool _hasPerfomedAction;
        protected bool _hasPerfomedComboAction;
        protected bool _willPerformCombo;

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
            if (!_hasPerfomedAction)
            {
                if (npc.ActionRecoveryTimer > 0f)
                {
                    return this;
                }
                if (!Action.InRangeToUse(npc))
                {
                    return SwitchState(npc, npc.CombatPhase.CurrentPhase.State);
                }
                if (Action.InAngleToUse(npc))
                {
                    PerformAction(npc);
                }
                return this;
            }
            if (!_hasPerfomedComboAction && _willPerformCombo)
            {
                if (!Action.ComboAction.InRangeToUse(npc))
                {
                    return SwitchState(npc, npc.CombatPhase.CurrentPhase.State);
                }
                if (Action.ComboAction.InAngleToUse(npc))
                {
                    PerformComboAction(npc);
                }
                return this;
            }
            return SwitchState(npc, npc.CombatPhase.CurrentPhase.State);
        }

        protected void PerformAction(AIController npc)
        {
            _hasPerfomedAction = true;
            Action.AttempToPerformAction(npc);
            //npc.ActionRecoveryTimer = Action.Cooldown;
            //npc.CurrentAction = Action;
            _willPerformCombo = Action.ComboAction != null && Action.ComboChance >= Random.value;

        }

        protected void PerformComboAction(AIController npc)
        {
            _hasPerfomedComboAction = true;
            Action.ComboAction.AttempToPerformAction(npc);
            //npc.ActionRecoveryTimer = Action.ComboAction.Cooldown;
            //npc.CurrentAction = Action.ComboAction;
        }

        protected override void ResetFlags(AIController npc)
        {
            base.ResetFlags(npc);
            _hasPerfomedAction = false;
            _hasPerfomedComboAction = false;
            _willPerformCombo = false;
        }
    }
}