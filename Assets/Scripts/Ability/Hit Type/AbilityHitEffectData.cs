using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Winter Universe/Ability/Hit Type/New Effect")]
    public class AbilityHitEffectData : AbilityHitTypeData
    {
        public List<EffectData> Effects = new();

        public override void Hit(Character caster, Character target, Vector3 position, Vector3 direction)
        {
            base.Hit(caster, target, position, direction);
            foreach (EffectData data in Effects)
            {
                Effect effect = data.CreateEffect();
                effect.Owner = target;
                effect.Source = caster;
                target.EffectModule.AddEffect(effect);
            }
        }
    }
}