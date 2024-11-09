using UnityEngine;

namespace WinterUniverse
{
    public class DamageColliderAOE : DamageCollider
    {
        public float Radius;

        public override void EnableDamageCollider()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, GameManager.StaticInstance.WorldLayer.PawnMask);
            foreach (Collider collider in colliders)
            {
                OnTriggerEnter(collider);
            }
        }
    }
}