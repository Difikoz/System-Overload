using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        [HideInInspector] public PawnController CurrentTarget;
        [HideInInspector] public float DistanceToTarget;
        [HideInInspector] public float AngleToTarget;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void HandleTargeting()
        {
            if (CurrentTarget != null && !_pawn.IsDead)
            {
                if (!CurrentTarget.IsDead)
                {
                    DistanceToTarget = Vector3.Distance(transform.position, CurrentTarget.transform.position);
                    AngleToTarget = ExtraTools.GetSignedAngleToDirection(transform.forward, CurrentTarget.transform.position - transform.position);
                }
                else
                {
                    SetTarget();
                }
            }
        }

        public void SetTarget(PawnController newTarget = null)
        {
            if (newTarget != null)
            {
                CurrentTarget = newTarget;
            }
            else
            {
                CurrentTarget = null;
                DistanceToTarget = float.MaxValue;
                AngleToTarget = 0f;
            }
        }

        public bool CurrentTargetInViewAngle()
        {
            return OtherTargetInViewAngle(CurrentTarget);
        }

        public bool OtherTargetInViewAngle(PawnController target)
        {
            return Vector3.Angle(_pawn.PawnAnimator.HeadPoint.forward, (target.PawnAnimator.BodyPoint.position - _pawn.PawnAnimator.HeadPoint.position).normalized) <= _pawn.PawnStats.ViewAngle.CurrentValue / 2f;
        }

        public bool CurrentTargetBlockedByObstacle()
        {
            return OtherTargetBlockedByObstacle(CurrentTarget);
        }

        public bool OtherTargetBlockedByObstacle(PawnController target)// if head or body not blocked - return false
        {
            return Physics.Linecast(_pawn.PawnAnimator.HeadPoint.position, target.PawnAnimator.BodyPoint.position, GameManager.StaticInstance.WorldLayer.ObstacleMask) && Physics.Linecast(_pawn.PawnAnimator.HeadPoint.position, target.PawnAnimator.HeadPoint.position, GameManager.StaticInstance.WorldLayer.ObstacleMask);
        }

        public bool CurrentTargetIsVisible()
        {
            return OtherTargetIsVisible(CurrentTarget);
        }

        public bool OtherTargetIsVisible(PawnController target)
        {
            return OtherTargetInViewAngle(target) && !OtherTargetBlockedByObstacle(target);
        }
    }
}