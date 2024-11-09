using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Winter Universe/Ability/Hit Type/New Effect")]
    public class AbilityHitEffectData : AbilityHitTypeData
    {
        public List<EffectCreator> Effects = new();

        public override void Hit(PawnController caster, PawnController target, Vector3 position, Vector3 direction)
        {
            base.Hit(caster, target, position, direction);
            foreach (EffectCreator creator in Effects)
            {
                if (creator.Chance >= Random.value)
                {
                    if (creator.OverrideDefaultValues)
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, caster, creator.Value, creator.Duration));
                    }
                    else
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, caster, creator.Effect.Value, creator.Effect.Duration));
                    }
                }
            }
        }
    }
}