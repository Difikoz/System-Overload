using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        public float EvadeEnergyCost = 10f;// TODO
        public float BlockEnergyCost = 10f;// TODO
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

        private void FixedUpdate()
        {
            if (CurrentTarget != null && !_pawn.IsDead)
            {
                if (_pawn.CanTargeting && CurrentTarget.IsTargetable && !CurrentTarget.IsDead)
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
            _pawn.IsCasting = true;
            while (_pawn.IsCasting && CastTime > 0f)
            {
                CastTime -= Time.deltaTime;
                yield return null;
            }
            CastTime = 0f;
            if (ability.CanCast(_pawn, CurrentTarget))
            {
                ability.CastComplete(_pawn, CurrentTarget, transform.position, transform.forward);
                _pawn.IsCasting = false;
            }
        }

        public void SetTarget(PawnController newTarget = null)
        {
            if (newTarget != null && _pawn.CanTargeting && newTarget.IsTargetable)
            {
                CurrentTarget = newTarget;
                _pawn.IsTargeting = true;
                //if (CurrentTarget.CharacterUI != null)
                //{
                //    CurrentTarget.CharacterUI.ShowBar();
                //}
            }
            else
            {
                CurrentTarget = null;
                _pawn.IsTargeting = false;
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

        public bool AttempToEvadeAttack()
        {
            if (_pawn.IsPerfomingAction)
            {
                return false;
            }
            if (_pawn.PawnStats.EnergyCurrent < EvadeEnergyCost)
            {
                return false;
            }
            if (_pawn.PawnStats.EvadeChance.CurrentValue / 100f <= Random.value)
            {
                return false;
            }
            _pawn.PawnStats.ReduceCurrentEnergy(EvadeEnergyCost);
            _pawn.PawnAnimator.PlayActionAnimation("Dodge Backward", true);
            return true;
        }

        public bool AttempToBlockAttack(float angle)
        {
            if (_pawn.IsPerfomingAction)
            {
                return false;
            }
            if (angle > _pawn.PawnStats.BlockAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (_pawn.PawnStats.BlockPower.CurrentValue <= 0f)
            {
                return false;
            }
            if (_pawn.PawnStats.EnergyCurrent < BlockEnergyCost)
            {
                return false;
            }
            if (_pawn.PawnStats.BlockChance.CurrentValue / 100f <= Random.value)
            {
                return false;
            }
            _pawn.PawnStats.ReduceCurrentEnergy(BlockEnergyCost);
            if (_pawn.PawnEquipment.WeaponRightSlot.Data != _pawn.PawnEquipment.UnarmedWeapon || _pawn.PawnEquipment.WeaponLeftSlot.Data == _pawn.PawnEquipment.UnarmedWeapon)
            {
                CurrentWeapon = _pawn.PawnEquipment.WeaponRightSlot.Data;
                CurrentSlotType = HandSlotType.Right;
                _pawn.PawnAnimator.PlayActionAnimation("Block Right", true);
            }
            else
            {
                CurrentWeapon = _pawn.PawnEquipment.WeaponLeftSlot.Data;
                CurrentSlotType = HandSlotType.Left;
                _pawn.PawnAnimator.PlayActionAnimation("Block Left", true);
            }
            return true;
        }
    }
}