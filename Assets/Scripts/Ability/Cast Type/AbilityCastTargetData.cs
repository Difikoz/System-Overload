using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Target", menuName = "Winter Universe/Ability/Cast Type/New Target")]
    public class AbilityCastTargetData : AbilityCastTypeData
    {
        public override bool CanCast(PawnController caster, PawnController target)
        {
            if (target == null)
            {
                return false;
            }
            return base.CanCast(caster, target);
        }

        public override void OnCastStart(PawnController caster, PawnController target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            base.OnCastStart(caster, target, position, direction, effect);
        }
    }
}