using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        public Action OnHitted;

        public bool Evadable = true;
        public bool Blockable = true;
        public List<DamageType> DamageTypes = new();
        public List<EffectCreator> TargetEffects = new();

        protected Collider _collider;
        protected Vector3 _hitPoint;
        protected Vector3 _hitDirection;
        protected float _angleFromTarget;
        protected float _angleFromHit;
        protected float _targetBlockPower;
        protected List<PawnController> _damagedCharacters = new();

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (CanDamageTarget(target))
            {
                _damagedCharacters.Add(target);
                _hitPoint = other.ClosestPointOnBounds(transform.position);
                _hitDirection = GetHitDirection(target);
                _angleFromTarget = GetAngleFromTarget(target);
                _angleFromHit = GetAngleFromHit(target);
                if (Evadable && _angleFromTarget <= 15f && target.PawnCombat.AttempToEvadeAttack())
                {
                    return;
                }
                if (Blockable && target.PawnCombat.AttempToBlockAttack(_angleFromTarget))
                {
                    _targetBlockPower = target.PawnStats.BlockPower.CurrentValue / 100f;
                }
                else
                {
                    _targetBlockPower = 0f;
                }
                ApplyDamageToTarget(target);
                OnHitted?.Invoke();
            }
        }

        protected virtual bool CanDamageTarget(PawnController target)
        {
            return target != null && !target.IsDead && !_damagedCharacters.Contains(target);
        }

        protected virtual Vector3 GetHitDirection(PawnController target)
        {
            return (_hitPoint - transform.position).normalized;
        }

        protected virtual float GetAngleFromTarget(PawnController target)
        {
            return Vector3.Angle(target.transform.forward, (_hitPoint - target.transform.position).normalized);
        }

        protected virtual float GetAngleFromHit(PawnController target)
        {
            return ExtraTools.GetSignedAngleToDirection(transform.forward, target.transform.forward);
        }

        protected virtual void ApplyDamageToTarget(PawnController target)
        {
            foreach (DamageType type in DamageTypes)
            {
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect();
                effect.Owner = target;
                effect.Value = type.Damage;
                effect.Value -= effect.Value * _targetBlockPower;
                effect.Element = type.Element;
                effect.HitPoint = _hitPoint;
                effect.HitDirection = _hitDirection;
                effect.AngleHitFrom = _angleFromHit;
                target.PawnEffects.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, TargetEffects);
        }

        protected virtual void ApplyEffectsToTarget(PawnController target, List<EffectCreator> effects)
        {
            if (target.IsDead)
            {
                return;
            }
            foreach (EffectCreator creator in effects)
            {
                if (creator.Chance > UnityEngine.Random.value)
                {
                    Effect effect = creator.Effect.CreateEffect();
                    effect.Owner = target;
                    if (creator.OverrideDefaultValues)
                    {
                        effect.Value = creator.Value;
                        effect.Duration = creator.Duration;
                    }
                    target.PawnEffects.AddEffect(effect);
                }
            }
        }

        public virtual void EnableDamageCollider()
        {
            _collider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            _collider.enabled = false;
            _damagedCharacters.Clear();
        }
    }
}