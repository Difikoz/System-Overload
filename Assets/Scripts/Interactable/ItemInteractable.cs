using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class ItemInteractable : Interactable
    {
        [HideInInspector] public ItemConfig Data;
        [HideInInspector] public int Amount = 1;

        private GameObject _model;

        public void Setup(ItemConfig data, int amount)
        {
            Data = data;
            Amount = amount;
            if (_model != null)
            {
                LeanPool.Despawn(_model);// TODO pool despawn
            }
            _model = LeanPool.Spawn(Data.Model, transform);// TODO pool spawn
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
            character.PawnInventory.AddItem(Data, Amount);
            if (character.GetComponent<PlayerController>())
            {
                GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification($"Added {(Amount > 1 ? $"{Amount} " : "")}{Data.DisplayName}");
            }
            LeanPool.Despawn(gameObject);// TODO pool despawn
        }
    }
}