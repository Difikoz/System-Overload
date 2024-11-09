using UnityEngine;

namespace WinterUniverse
{
    public class NPCLocomotionModule : LocomotionModule
    {
        private NPCController _npc;

        protected override void Awake()
        {
            base.Awake();
            _npc = GetComponent<NPCController>();
        }

        protected override Vector2 GetMoveInput()
        {
            if (!_npc.ReachedDestination)
            {
                return new Vector2(Vector3.Dot(_npc.Agent.desiredVelocity, transform.right), Vector3.Dot(_npc.Agent.desiredVelocity, transform.forward)).normalized;
            }
            return Vector2.zero;
        }

        protected override Vector3 GetLookDirection()
        {
            if (_npc.CombatModule.CurrentTarget != null && _npc.CombatModule.CurrentTargetIsVisible())
            {
                return (_npc.CombatModule.CurrentTarget.transform.position - transform.position).normalized;
            }
            else if (!_npc.ReachedDestination)
            {
                return _npc.Agent.desiredVelocity;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}