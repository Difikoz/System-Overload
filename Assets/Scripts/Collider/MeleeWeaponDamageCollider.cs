using UnityEngine;

namespace WinterUniverse
{
    public class MeleeWeaponDamageCollider : OwnerBasedDamageCollider
    {


        protected override Vector3 GetHitDirection(Character target)
        {
            return (target.transform.position - Owner.transform.position).normalized;
        }

        protected override float GetAngleFromTarget(Character target)
        {
            return Vector3.Angle(target.transform.forward, (Owner.transform.position - target.transform.position).normalized);
        }

        protected override float GetAngleFromHit(Character target)
        {
            return ExtraTools.GetSignedAngleToDirection(Owner.transform.forward, target.transform.forward);
        }
    }
}