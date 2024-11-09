using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Winter Universe/Ability/Cast Type/New AOE")]
    public class AbilityCastAOEData : AbilityCastTypeData
    {
        public float Radius = 4f;

        public override void OnCastStart(Character caster, Character target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            base.OnCastStart(caster, target, position, direction, effect);
        }
    }
}