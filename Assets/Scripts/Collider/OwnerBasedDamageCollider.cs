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

        protected override bool CanDamageTarget(PawnController target, out PawnController source)
        {
            source = Owner;
            return target != null && !target.IsDead && target != Owner && !_damagedCharacters.Contains(target);
        }

        protected override void ApplyDamageToTarget(PawnController target, PawnController source)
        {
            ApplyEffectsToTarget(Owner, source, OwnerEffects);
            foreach (DamageType type in DamageTypes)
            {
                float damage = type.Damage + Owner.PawnStats.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue + Owner.PawnStats.GetStatByName(type.Element.DamageType.DisplayName).CurrentValue;
                damage *= Owner.PawnStats.DamageDealt.CurrentValue / 100f;
                damage -= damage * _targetBlockPower;
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect(target, source, damage, 0f);
                effect.Initialize(type.Element, _hitPoint, _hitDirection, _angleFromHit);
                target.PawnEffects.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, source, TargetEffects);
        }
    }
}