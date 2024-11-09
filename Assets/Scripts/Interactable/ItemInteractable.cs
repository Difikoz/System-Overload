using UnityEngine;

namespace WinterUniverse
{
    public class ItemInteractable : Interactable
    {
        [HideInInspector] public ItemData Data;
        [HideInInspector] public int Amount = 1;

        private GameObject _model;

        public void Setup(ItemData data, int amount)
        {
            Data = data;
            Amount = amount;
            if (_model != null)
            {
                Destroy(_model);// TODO pool despawn
            }
            _model = Instantiate(Data.Model, transform);// TODO pool spawn
        }

        public override string GetInteractionMessage()
        {
            return $"Pick Up {(Amount > 1 ? $"{Amount} " : "")}{Data.DisplayName}";
        }

        public override bool CanInteract(PawnController character)
        {
            return true;
        }

        public override void Interact(PawnController character)
        {
            character.InventoryModule.AddItem(Data, Amount);
            if (character.GetComponent<PlayerController>())
            {
                PlayerUIManager.StaticInstance.HUD.NotificationUI.DisplayNotification($"Added {(Amount > 1 ? $"{Amount} " : "")}{Data.DisplayName}");
            }
            Destroy(gameObject);// TODO pool despawn
        }
    }
}