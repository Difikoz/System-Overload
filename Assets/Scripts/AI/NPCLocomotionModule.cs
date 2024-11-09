using UnityEngine;

namespace WinterUniverse
{
    public class NPCLocomotionModule : PawnLocomotion
    {
        private NPCController _ai;

        public override void Initialize()
        {
            base.Initialize();
            _ai = GetComponent<NPCController>();
        }

        protected override Vector2 GetMoveInput()
        {
            if (!_ai.ReachedDestination)
            {
                return new Vector2(Vector3.Dot(_ai.Agent.desiredVelocity, transform.right), Vector3.Dot(_ai.Agent.desiredVelocity, transform.forward)).normalized;
            }
            return Vector2.zero;
        }

        protected override Vector3 GetLookDirection()
        {
            if (_ai.PawnCombat.CurrentTarget != null && _ai.PawnCombat.CurrentTargetIsVisible())
            {
                return (_ai.PawnCombat.CurrentTarget.transform.position - transform.position).normalized;
            }
            else if (!_ai.ReachedDestination)
            {
                return _ai.Agent.desiredVelocity;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}