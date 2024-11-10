using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class OwnerBasedDamageColliderAOE : DamageColliderAOE
    {
        [HideInInspector] public PawnController Owner;
        [HideInInspector] public List<EffectCreator> OwnerEffects = new();

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
                InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.WorldData.HealthReduceEffect.CreateEffect(target, source, Owner.PawnStats.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue * Owner.PawnStats.DamageDealt.CurrentValue / 100f + type.Damage, 0f);
                effect.Initialize(type.Element, _hitPoint, _hitDirection, _angleFromHit);
                target.PawnEffects.AddEffect(effect);
            }
            ApplyEffectsToTarget(target, source, TargetEffects);
        }
    }
}