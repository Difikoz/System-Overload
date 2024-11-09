using UnityEngine;

namespace WinterUniverse
{
    public class AbilityHitTypeData : ScriptableObject
    {
        public virtual bool CanHit(PawnController caster, PawnController target)
        {
            return true;
        }

        public virtual void Hit(PawnController caster, PawnController target, Vector3 position, Vector3 direction)
        {

        }
    }
}