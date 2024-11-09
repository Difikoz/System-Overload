using UnityEngine;

namespace WinterUniverse
{
    public class LayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private LayerMask _characterMask;
        [SerializeField] private LayerMask _damageableMask;

        public LayerMask ObstacleMask => _obstacleMask;
        public LayerMask CharacterMask => _characterMask;
        public LayerMask DamageableMask => _damageableMask;
    }
}