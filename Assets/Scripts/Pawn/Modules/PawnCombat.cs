using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        public float HearRadius = 5f;// TODO create stat
        public float ViewDistance = 40f;// TODO create stat
        public float ViewAngle = 90f;// TODO create stat

        public Transform HeadPoint;
        public Transform BodyPoint;
        public Transform FootRightPoint;
        public Transform FootLeftPoint;

        [HideInInspector] public PawnController CurrentTarget;
        [HideInInspector] public WeaponItemConfig CurrentWeapon;
        [HideInInspector] public HandSlotType CurrentSlotType;

        [HideInInspector] public float DistanceToTarget;
        [HideInInspector] public float AngleToTarget;
        [HideInInspector] public float CastTime;

        public virtual void Initialize()
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

        public void UseWeaponAbility(WeaponItemConfig weapon, HandSlotType slot, AbilityData ability)
        {
            if (ability.CanCast(_pawn, CurrentTarget))
            {
                ability.CastStart(_pawn, CurrentTarget, transform.position, transform.forward);
                StartCoroutine(CastAbilityTimer(ability));
            }
        }

        public void UseSpellAbility(AbilityData ability)
        {
            if (ability != null && ability.CanCast(_pawn, CurrentTarget))
            {
                ability.CastStart(_pawn, CurrentTarget, transform.position, transform.forward);
                StartCoroutine(CastAbilityTimer(ability));
            }
        }

        private IEnumerator CastAbilityTimer(AbilityData ability)
        {
            CastTime = ability.CastTime;
            while (CastTime > 0f)
            {
                CastTime -= Time.deltaTime;
                yield return null;
            }
            CastTime = 0f;
            if (ability.CanCast(_pawn, CurrentTarget))
            {
                ability.CastComplete(_pawn, CurrentTarget, transform.position, transform.forward);
            }
        }

        public void SetTarget(PawnController newTarget = null)
        {
            if (newTarget != null)
            {
                CurrentTarget = newTarget;
                //if (CurrentTarget.CharacterUI != null)
                //{
                //    CurrentTarget.CharacterUI.ShowBar();
                //}
            }
            else
            {
                CurrentTarget = null;
                DistanceToTarget = float.MaxValue;
            }
        }

        public bool CurrentTargetInViewAngle()
        {
            return TargetInViewAngle(CurrentTarget);
        }

        public bool TargetInViewAngle(PawnController cm)
        {
            //Debug.Log(Vector3.Angle(HeadPoint.forward, (cm.CharacterCombatManager.BodyPoint.position - HeadPoint.position).normalized));
            return Vector3.Angle(HeadPoint.forward, (cm.PawnCombat.BodyPoint.position - HeadPoint.position).normalized) <= ViewAngle / 2f;// TODO
        }

        public bool CurrentTargetBlockedByObstacle()
        {
            return TargetBlockedByObstacle(CurrentTarget);
        }

        public bool TargetBlockedByObstacle(PawnController cm)
        {
            return Physics.Linecast(HeadPoint.position, cm.PawnCombat.BodyPoint.position, GameManager.StaticInstance.WorldLayer.ObstacleMask);
        }

        public bool CurrentTargetIsVisible()
        {
            return TargetIsVisible(CurrentTarget);
        }

        public bool TargetIsVisible(PawnController cm)
        {
            return TargetInViewAngle(cm) && !TargetBlockedByObstacle(cm);
        }
    }
}