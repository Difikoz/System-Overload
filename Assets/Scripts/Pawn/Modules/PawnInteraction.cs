using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInteraction : MonoBehaviour
    {
        public Action<List<Interactable>> OnRefreshInteractables;

        private PawnController _pawn;
        private List<Interactable> _interactables = new();

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
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

        public void RefreshInteractables()
        {
            for (int i = _interactables.Count - 1; i >= 0; i--)
            {
                if (_interactables[i] == null || !_interactables[i].gameObject.activeSelf)
                {
                    _interactables.RemoveAt(i);
                }
            }
            OnRefreshInteractables?.Invoke(_interactables);
        }

        public void Interact()
        {
            if (_pawn.IsPerfomingAction)
            {
                return;
            }
            RefreshInteractables();
            if (_interactables.Count > 0)
            {
                if (_interactables[0].CanInteract(_pawn))
                {
                    _interactables[0].Interact(_pawn);
                    _interactables.RemoveAt(0);
                    RefreshInteractables();
                }
            }
        }
    }
}