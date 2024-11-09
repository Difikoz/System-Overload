using UnityEngine;

namespace WinterUniverse
{
    public class MeleeWeaponDamageCollider : OwnerBasedDamageCollider
    {


        protected override Vector3 GetHitDirection(PawnController target)
        {
            return (target.transform.position - Owner.transform.position).normalized;
        }

        protected override float GetAngleFromTarget(PawnController target)
        {
            return Vector3.Angle(target.transform.forward, (Owner.transform.position - target.transform.position).normalized);
        }

        protected override float GetAngleFromHit(PawnController target)
        {
            return ExtraTools.GetSignedAngleToDirection(Owner.transform.forward, target.transform.forward);
        }
    }
}