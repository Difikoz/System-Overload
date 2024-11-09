using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class OwnerBasedDamageCollider : DamageCollider
    {
        [HideInInspector] public PawnController Owner;
        [HideInInspector] public List<EffectCreator> OwnerEffects = new();

        public virtual void Setup(WeaponSlot slot)
        {
            Owner = slot.Owner;
            DamageTypes = new(slot.Data.DamageTypes);
            OwnerEffects = new(slot.Data.OwnerEffects);
            TargetEffects = new(slot.Data.TargetEffects);
        }

        protected override bool CanDamageTarget(PawnController target)
        {
            return target != null && !target.IsDead && target != Owner && !_damagedCharacters.Contains(target);
        }

        protected override void ApplyDamageToTarget(PawnController target)
        {
            ApplyEffectsToTarget(Owner, OwnerEffects);
            foreach (DamageType type in DamageTypes)
            {
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect();
                effect.Owner = target;
                effect.Source = Owner;
                effect.Value = type.Damage + Owner.PawnStats.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue + Owner.PawnStats.GetStatByName(type.Element.DamageType.DisplayName).CurrentValue;
                effect.Value *= Owner.PawnStats.DamageDealt.CurrentValue / 100f;
                effect.Value -= effect.Value * _targetBlockPower;
                effect.Element = type.Element;
                effect.AngleHitFrom = _angleFromHit;
                effect.HitPoint = _hitPoint;
                effect.HitDirection = _hitDirection;
                target.PawnEffects.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, TargetEffects);
        }
    }
}