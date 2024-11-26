using UnityEngine;

namespace WinterUniverse
{
    public class WorldLayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private LayerMask _pawnMask;
        [SerializeField] private LayerMask _detectableMask;

        public LayerMask ObstacleMask => _obstacleMask;
        public LayerMask PawnMask => _pawnMask;
        public LayerMask DetectableMask => _detectableMask;
    }
}