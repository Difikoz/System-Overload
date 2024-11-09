using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(InventoryModule))]
    public class ChestInteractable : Interactable
    {
        [SerializeField] private string _interactionMessage = "Open Chest";
        [HideInInspector] public InventoryModule Inventory;
        public bool DespawnOnEmpty;

        protected override void Awake()
        {
            base.Awake();
            Inventory = GetComponent<InventoryModule>();
        }

        public override string GetInteractionMessage()
        {
            return _interactionMessage;
        }

        public override bool CanInteract(Character character)
        {
            return true;
        }

        public override void Interact(Character character)
        {

        }
    }
}