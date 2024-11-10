using UnityEngine;

namespace WinterUniverse
{
    public class AltarInteractable : Interactable
    {
        [SerializeField] private string _interactableMessage = "Restore Power";
        [SerializeField] private string _notificationMessage = "Power Restored";
        [SerializeField] private Transform _respawnPoint;

        public override string GetInteractionMessage()
        {
            return _interactableMessage;
        }

        public override bool CanInteract(PawnController character)
        {
            return character.GetComponent<PlayerController>() != null;
        }

        public override void Interact(PawnController character)
        {
            character.PawnEffects.RemoveNegativeEffects();
            character.PawnStats.RestoreCurrentHealth(character.PawnStats.HealthMax.CurrentValue);
            character.PawnStats.RestoreCurrentEnergy(character.PawnStats.EnergyMax.CurrentValue);
            GameManager.StaticInstance.WorldSaveLoad.CurrentSaveData.RespawnTransform.SetPositionAndRotation(_respawnPoint.position, _respawnPoint.eulerAngles);
            GameManager.StaticInstance.WorldSaveLoad.SaveGame();
            GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification(_notificationMessage);
        }
    }
}