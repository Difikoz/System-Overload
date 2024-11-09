using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityCastProjectileData : AbilityCastTypeData
    {
        public GameObject ProjectilePrefab;
        public int ProjectileCount = 1;
        public float ProjectileSpread = 5f;
        public float ProjectileForce = 25;

        public override void OnCastStart(Character caster, Character target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            base.OnCastStart(caster, target, position, direction, effect);
        }
    }
}