using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private PawnController _pawn;

        private void Awake()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void SetTrigger(string name)// in future change to play action
        {
            _animator.SetTrigger(name);
        }
    }
}