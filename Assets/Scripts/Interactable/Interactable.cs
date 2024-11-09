using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string GetInteractionMessage();
        public abstract bool CanInteract(PawnController character);
        public abstract void Interact(PawnController character);

        protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PawnController character))
            {
                OnEnter(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PawnController character))
            {
                OnExit(character);
            }
        }

        protected virtual void OnEnter(PawnController character)
        {
            character.InteractionModule.AddInteractable(this);
        }

        protected virtual void OnExit(PawnController character)
        {
            character.InteractionModule.RemoveInteractable(this);
        }
    }
}