using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string GetInteractionMessage();
        public abstract bool CanInteract(Character character);
        public abstract void Interact(Character character);

        protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                OnEnter(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                OnExit(character);
            }
        }

        protected virtual void OnEnter(Character character)
        {
            character.InteractionModule.AddInteractable(this);
        }

        protected virtual void OnExit(Character character)
        {
            character.InteractionModule.RemoveInteractable(this);
        }
    }
}