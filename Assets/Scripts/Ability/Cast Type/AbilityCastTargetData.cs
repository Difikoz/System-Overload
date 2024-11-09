using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Target", menuName = "Winter Universe/Ability/Cast Type/New Target")]
    public class AbilityCastTargetData : AbilityCastTypeData
    {
        public override bool CanCast(Character caster, Character target)
        {
            if (target == null)
            {
                return false;
            }
            return base.CanCast(caster, target);
        }

        public override void OnCastStart(Character caster, Character target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            base.OnCastStart(caster, target, position, direction, effect);
        }
    }
}