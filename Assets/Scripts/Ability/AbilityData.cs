using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Winter Universe/Ability/New Ability")]
    public class AbilityData : ScriptableObject
    {
        [Header("Basic Information")]
        public string DisplayName = "Fireball";
        [TextArea] public string Description = "Cast Fireball";
        public Sprite Icon;
        public AbilityCastTypeData CastType;
        public AbilityHitTypeData EffectType;
        public float CastTime = 0.1f;

        public bool CanCast(Character caster, Character target)
        {
            return CastType.CanCast(caster, target);
        }

        public void CastStart(Character caster, Character target, Vector3 position, Vector3 direction)
        {
            CastType.OnCastStart(caster, target, position, direction, EffectType);
        }

        public void CastComplete(Character caster, Character target, Vector3 position, Vector3 direction)
        {
            CastType.OnCastStart(caster, target, position, direction, EffectType);
        }
    }
}