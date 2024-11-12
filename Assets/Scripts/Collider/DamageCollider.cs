using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        public Action OnHitted;

        [SerializeField] protected Collider _collider;
        [SerializeField] protected List<DamageType> _damageTypes = new();
        [SerializeField] protected List<EffectCreator> _targetEffects = new();
        [SerializeField] protected bool _doSplashDamage;
        [SerializeField] protected float _splashRadius;

        protected PawnController _owner;
        protected List<EffectCreator> _ownerEffects = new();
        protected Vector3 _hitPoint;
        protected Vector3 _hitDirection;
        protected List<PawnController> _damagedCharacters = new();

        public virtual void Initialize(PawnController owner, List<DamageType> damageTypes, List<EffectCreator> ownerEffects, List<EffectCreator> targetEffects, bool doSplashDamage, float splashRadius)
        {
            _owner = owner;
            _damageTypes = damageTypes;
            _ownerEffects = ownerEffects;
            _targetEffects = targetEffects;
            _doSplashDamage = doSplashDamage;
            _splashRadius = splashRadius;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (_doSplashDamage)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _splashRadius, GameManager.StaticInstance.WorldLayer.PawnMask);
                foreach (Collider collider in colliders)
                {
                    if (CanDamageTarget(target))
                    {
                        _damagedCharacters.Add(target);
                        _hitPoint = GetHitPoint(other);
                        _hitDirection = GetHitDirection(target);
                        if (_owner != null)
                        {
                            ApplyEffectsToTarget(_owner, _ownerEffects);
                        }
                        ApplyDamageToTarget(target);
                        ApplyEffectsToTarget(target, _targetEffects);
                        OnHitted?.Invoke();
                    }
                }
            }
            else if (CanDamageTarget(target))
            {
                _damagedCharacters.Add(target);
                _hitPoint = GetHitPoint(other);
                _hitDirection = GetHitDirection(target);
                if (_owner != null)
                {
                    ApplyEffectsToTarget(_owner, _ownerEffects);
                }
                ApplyDamageToTarget(target);
                ApplyEffectsToTarget(target, _targetEffects);
                OnHitted?.Invoke();
            }
        }

        protected virtual bool CanDamageTarget(PawnController target)
        {
            if (_owner != null && _owner == target)
            {
                return false;
            }
            return target != null && !target.IsDead && !_damagedCharacters.Contains(target);
        }

        protected Vector3 GetHitPoint(Collider other)
        {
            return other.ClosestPointOnBounds(transform.position);
        }

        protected virtual Vector3 GetHitDirection(PawnController target)
        {
            if (_owner != null)
            {
                return (target.transform.position - _owner.transform.position).normalized;
            }
            return (_hitPoint - transform.position).normalized;
        }

        protected virtual void ApplyDamageToTarget(PawnController target)
        {
            if (_owner != null)
            {
                foreach (DamageType type in _damageTypes)
                {
                    InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect(target, _owner, _owner.PawnStats.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue * _owner.PawnStats.DamageDealt.CurrentValue / 100f + type.Damage, 0f);
                    effect.Initialize(type.Element, _hitPoint, _hitDirection);
                    target.PawnEffects.AddEffect(effect);
                }
            }
            else
            {
                foreach (DamageType type in _damageTypes)
                {
                    InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect(target, _owner, type.Damage, 0f);
                    effect.Initialize(type.Element, _hitPoint, _hitDirection);
                    target.PawnEffects.AddEffect(effect);
                }
            }
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
                    if (creator.OverrideDefaultValues)
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, _owner, creator.Value, creator.Duration));
                    }
                    else
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, _owner, creator.Effect.Value, creator.Effect.Duration));
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