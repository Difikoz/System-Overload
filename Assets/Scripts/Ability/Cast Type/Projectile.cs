using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class Projectile : MonoBehaviour
    {
        public DamageCollider ProjectileCollider;
        public int PierceCount;

        private int _currentPierceCount;

        public void Shoot()
        {
            _currentPierceCount = 0;
            ProjectileCollider.OnHitted += OnHitted;
            ProjectileCollider.EnableDamageCollider();
        }

        private void OnHitted()
        {
            if (_currentPierceCount == PierceCount)
            {
                ProjectileCollider.OnHitted -= OnHitted;
                LeanPool.Despawn(gameObject);// TODO pool despawn
            }
            else
            {
                _currentPierceCount++;
            }
        }
    }
}