using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractionModule : MonoBehaviour
    {
        private Character _owner;
        protected List<Interactable> _interactables = new();

        protected virtual void Awake()
        {
            _owner = GetComponent<Character>();
        }

        public void AddInteractable(Interactable interactable)
        {
            if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
            }
            RefreshInteractables();
        }

        public void RemoveInteractable(Interactable interactable)
        {
            if (_interactables.Contains(interactable))
            {
                _interactables.Remove(interactable);
            }
            RefreshInteractables();
        }

        public virtual void RefreshInteractables()
        {
            for (int i = _interactables.Count - 1; i >= 0; i--)
            {
                if (_interactables[i] == null || !_interactables[i].gameObject.activeSelf)
                {
                    _interactables.RemoveAt(i);
                }
            }
        }

        public void Interact()
        {
            if (_owner.IsPerfomingAction)
            {
                return;
            }
            RefreshInteractables();
            if (_interactables.Count > 0)
            {
                if (_interactables[0].CanInteract(_owner))
                {
                    _interactables[0].Interact(_owner);
                    _interactables.RemoveAt(0);
                    RefreshInteractables();
                }
            }
        }
    }
}