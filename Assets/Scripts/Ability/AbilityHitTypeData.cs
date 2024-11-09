using UnityEngine;

namespace WinterUniverse
{
    public class AbilityHitTypeData : ScriptableObject
    {
        public virtual bool CanHit(Character caster, Character target)
        {
            return true;
        }

        public virtual void Hit(Character caster, Character target, Vector3 position, Vector3 direction)
        {

        }
    }
}