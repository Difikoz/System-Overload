using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnInventory))]
    public class ChestInteractable : Interactable
    {
        [SerializeField] private string _interactionMessage = "Open Chest";
        [HideInInspector] public PawnInventory Inventory;
        public bool DespawnOnEmpty;

        protected override void Awake()
        {
            base.Awake();
            Inventory = GetComponent<PawnInventory>();
        }

        public override string GetInteractionMessage()
        {
            return _interactionMessage;
        }

        public override bool CanInteract(PawnController character)
        {
            return true;
        }

        public override void Interact(PawnController character)
        {

        }
    }
}