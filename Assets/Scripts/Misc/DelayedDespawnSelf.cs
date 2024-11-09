using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class DelayedDespawnSelf : MonoBehaviour
    {
        [SerializeField] private float _delay = 5f;

        private void Awake()
        {
            LeanPool.Despawn(gameObject, _delay);
        }
    }
}