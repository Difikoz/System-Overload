using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        public Action OnHitted;

        public List<DamageType> DamageTypes = new();
        public List<EffectCreator> TargetEffects = new();

        protected Collider _collider;
        protected Vector3 _hitPoint;
        protected Vector3 _hitDirection;
        protected float _angleFromTarget;
        protected float _angleFromHit;
        protected List<PawnController> _damagedCharacters = new();

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (CanDamageTarget(target, out PawnController source))
            {
                _damagedCharacters.Add(target);
                _hitPoint = other.ClosestPointOnBounds(transform.position);
                _hitDirection = GetHitDirection(target);
                _angleFromTarget = GetAngleFromTarget(target);
                _angleFromHit = GetAngleFromHit(target);
                ApplyDamageToTarget(target, source);
                OnHitted?.Invoke();
            }
        }

        protected virtual bool CanDamageTarget(PawnController target, out PawnController source)
        {
            source = null;
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

        protected virtual void ApplyDamageToTarget(PawnController target, PawnController source)
        {
            foreach (DamageType type in DamageTypes)
            {
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect(target, source, type.Damage, 0f);
                effect.Initialize(type.Element, _hitPoint, _hitDirection, _angleFromHit);
                target.PawnEffects.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, source, TargetEffects);
        }

        protected virtual void ApplyEffectsToTarget(PawnController target, PawnController source, List<EffectCreator> effects)
        {
            if (target.IsDead)
            {
                return;
            }
            foreach (EffectCreator creator in effects)
            {
                if (creator.Chance > UnityEngine.Random.value)
                {
                    if (creator.OverrideDefaultValues)
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, source, creator.Value, creator.Duration));
                    }
                    else
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, source, creator.Effect.Value, creator.Effect.Duration));
                    }
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