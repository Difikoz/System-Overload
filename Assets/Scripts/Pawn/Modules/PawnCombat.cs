using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        [HideInInspector] public PawnController CurrentTarget;
        [HideInInspector] public WeaponItemConfig CurrentWeapon;
        [HideInInspector] public AttackType CurrentAttackType;
        [HideInInspector] public HandSlotType CurrentSlotType;

        [HideInInspector] public float DistanceToTarget;
        [HideInInspector] public float AngleToTarget;
        [HideInInspector] public float CastTime;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
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

        public void UseWeaponAction(WeaponItemConfig weapon, AttackType type, HandSlotType slot)
        {
            CurrentWeapon = weapon;
            CurrentAttackType = type;
            CurrentSlotType = slot;
            switch (CurrentAttackType)
            {
                case AttackType.Primary:
                    CurrentWeapon.PrimaryAction.AttempToPerformAction(_pawn);
                    break;
                case AttackType.Secondary:
                    CurrentWeapon.SecondaryAction.AttempToPerformAction(_pawn);
                    break;
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

        public bool TargetInViewAngle(PawnController target)
        {
            //Debug.Log(Vector3.Angle(HeadPoint.forward, (cm.CharacterCombatManager.BodyPoint.position - HeadPoint.position).normalized));
            return Vector3.Angle(_pawn.PawnAnimator.HeadPoint.forward, (target.PawnAnimator.BodyPoint.position - _pawn.PawnAnimator.HeadPoint.position).normalized) <= _pawn.PawnStats.ViewAngle.CurrentValue / 2f;// TODO
        }

        public bool CurrentTargetBlockedByObstacle()
        {
            return TargetBlockedByObstacle(CurrentTarget);
        }

        public bool TargetBlockedByObstacle(PawnController target)
        {
            return Physics.Linecast(_pawn.PawnAnimator.HeadPoint.position, target.PawnAnimator.BodyPoint.position, GameManager.StaticInstance.WorldLayer.ObstacleMask);
        }

        public bool CurrentTargetIsVisible()
        {
            return TargetIsVisible(CurrentTarget);
        }

        public bool TargetIsVisible(PawnController target)
        {
            return TargetInViewAngle(target) && !TargetBlockedByObstacle(target);
        }
    }
}