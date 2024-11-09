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

        public override bool CanInteract(Character character)
        {
            return character.GetComponent<PlayerController>() != null;
        }

        public override void Interact(Character character)
        {
            character.EffectModule.RemoveNegativeEffects();
            character.StatModule.RestoreCurrentHealth(character.StatModule.HealthMax.CurrentValue);
            character.StatModule.RestoreCurrentEnergy(character.StatModule.EnergyMax.CurrentValue);
            WorldSaveGameManager.StaticInstance.CurrentSaveData.RespawnTransform.SetPositionAndRotation(_respawnPoint.position, _respawnPoint.eulerAngles);
            WorldSaveGameManager.StaticInstance.SaveGame();
            PlayerUIManager.StaticInstance.HUD.NotificationUI.DisplayNotification(_notificationMessage);
        }
    }
}