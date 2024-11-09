using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class OwnerBasedDamageColliderAOE : DamageColliderAOE
    {
        [HideInInspector] public Character Owner;
        [HideInInspector] public List<EffectCreator> OwnerEffects = new();

        protected override bool CanDamageTarget(Character target)
        {
            return target != null && !target.IsDead && target != Owner && !_damagedCharacters.Contains(target);
        }

        protected override void ApplyDamageToTarget(Character target)
        {
            ApplyEffectsToTarget(Owner, OwnerEffects);
            foreach (DamageType type in DamageTypes)
            {
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)WorldDataManager.StaticInstance.HealthReduceEffect.CreateEffect();
                effect.Owner = target;
                effect.Source = Owner;
                effect.Value = type.Damage + Owner.StatModule.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue + Owner.StatModule.GetStatByName(type.Element.DamageType.DisplayName).CurrentValue;
                effect.Value *= Owner.StatModule.DamageDealt.CurrentValue / 100f;
                effect.Value -= effect.Value * _targetBlockPower;
                effect.Element = type.Element;
                effect.AngleHitFrom = _angleFromHit;
                effect.HitPoint = _hitPoint;
                effect.HitDirection = _hitDirection;
                target.EffectModule.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, TargetEffects);
        }
    }
}