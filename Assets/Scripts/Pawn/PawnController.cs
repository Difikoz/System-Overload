using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        [SerializeField] private PawnAnimator _pawnAnimator;
        [SerializeField] private PawnLocomotion _pawnLocomotion;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
    }
}