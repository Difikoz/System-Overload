using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class CombatModule : MonoBehaviour
    {
        private PawnController _owner;

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
        [HideInInspector] public WeaponItemData CurrentWeapon;
        [HideInInspector] public HandSlotType CurrentSlotType;

        [HideInInspector] public float DistanceToTarget;
        [HideInInspector] public float AngleToTarget;
        [HideInInspector] public float CastTime;

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        private void FixedUpdate()
        {
            if (CurrentTarget != null && !_owner.IsDead)
            {
                if (_owner.CanTargeting && CurrentTarget.IsTargetable && !CurrentTarget.IsDead)
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

        public void UseWeaponAbility(WeaponItemData weapon, HandSlotType slot, AbilityData ability)
        {
            if (ability.CanCast(_owner, CurrentTarget))
            {
                ability.CastStart(_owner, CurrentTarget, transform.position, transform.forward);
                StartCoroutine(CastAbilityTimer(ability));
            }
        }

        public void UseSpellAbility(AbilityData ability)
        {
            if (ability != null && ability.CanCast(_owner, CurrentTarget))
            {
                ability.CastStart(_owner, CurrentTarget, transform.position, transform.forward);
                StartCoroutine(CastAbilityTimer(ability));
            }
        }

        private IEnumerator CastAbilityTimer(AbilityData ability)
        {
            CastTime = ability.CastTime;
            _owner.IsCasting = true;
            while (_owner.IsCasting && CastTime > 0f)
            {
                CastTime -= Time.deltaTime;
                yield return null;
            }
            CastTime = 0f;
            if (ability.CanCast(_owner, CurrentTarget))
            {
                ability.CastComplete(_owner, CurrentTarget, transform.position, transform.forward);
                _owner.IsCasting = false;
            }
        }

        public void SetTarget(PawnController newTarget = null)
        {
            if (newTarget != null && _owner.CanTargeting && newTarget.IsTargetable)
            {
                CurrentTarget = newTarget;
                _owner.IsTargeting = true;
                //if (CurrentTarget.CharacterUI != null)
                //{
                //    CurrentTarget.CharacterUI.ShowBar();
                //}
            }
            else
            {
                CurrentTarget = null;
                _owner.IsTargeting = false;
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
            return Vector3.Angle(HeadPoint.forward, (cm.CombatModule.BodyPoint.position - HeadPoint.position).normalized) <= ViewAngle / 2f;// TODO
        }

        public bool CurrentTargetBlockedByObstacle()
        {
            return TargetBlockedByObstacle(CurrentTarget);
        }

        public bool TargetBlockedByObstacle(PawnController cm)
        {
            return Physics.Linecast(HeadPoint.position, cm.CombatModule.BodyPoint.position, GameManager.StaticInstance.WorldLayer.ObstacleMask);
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
            if (_owner.IsPerfomingAction)
            {
                return false;
            }
            if (_owner.StatModule.EnergyCurrent < EvadeEnergyCost)
            {
                return false;
            }
            if (_owner.StatModule.EvadeChance.CurrentValue / 100f <= Random.value)
            {
                return false;
            }
            _owner.StatModule.ReduceCurrentEnergy(EvadeEnergyCost);
            _owner.AnimatorModule.PlayActionAnimation("Dodge Backward", true);
            return true;
        }

        public bool AttempToBlockAttack(float angle)
        {
            if (_owner.IsPerfomingAction)
            {
                return false;
            }
            if (angle > _owner.StatModule.BlockAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (_owner.StatModule.BlockPower.CurrentValue <= 0f)
            {
                return false;
            }
            if (_owner.StatModule.EnergyCurrent < BlockEnergyCost)
            {
                return false;
            }
            if (_owner.StatModule.BlockChance.CurrentValue / 100f <= Random.value)
            {
                return false;
            }
            _owner.StatModule.ReduceCurrentEnergy(BlockEnergyCost);
            if (_owner.EquipmentModule.WeaponRightSlot.Data != _owner.EquipmentModule.UnarmedWeapon || _owner.EquipmentModule.WeaponLeftSlot.Data == _owner.EquipmentModule.UnarmedWeapon)
            {
                CurrentWeapon = _owner.EquipmentModule.WeaponRightSlot.Data;
                CurrentSlotType = HandSlotType.Right;
                _owner.AnimatorModule.PlayActionAnimation("Block Right", true);
            }
            else
            {
                CurrentWeapon = _owner.EquipmentModule.WeaponLeftSlot.Data;
                CurrentSlotType = HandSlotType.Left;
                _owner.AnimatorModule.PlayActionAnimation("Block Left", true);
            }
            return true;
        }
    }
}