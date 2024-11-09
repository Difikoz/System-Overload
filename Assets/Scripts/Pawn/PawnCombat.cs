using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }
    }
}