using UnityEngine;

namespace WinterUniverse
{
    public class PlayerUIManager : MonoBehaviour
    {
        private PlayerVitalityBarUI _vitalityUI;
        private PlayerNotificationUI _notificationUI;
        private PlayerInteractionUI _interactionUI;
        private PlayerStatUI _statUI;
        private PlayerEquipmentUI _equipmentUI;
        private PlayerInventoryUI _inventoryUI;
        private PlayerFactionUI _factionUI;

        [SerializeField] private GameObject _menuWindow;

        public PlayerNotificationUI NotificationUI => _notificationUI;

        public void Initialize()
        {
            _vitalityUI = GetComponentInChildren<PlayerVitalityBarUI>();
            _notificationUI = GetComponentInChildren<PlayerNotificationUI>();
            _interactionUI = GetComponentInChildren<PlayerInteractionUI>();
            _statUI = GetComponentInChildren<PlayerStatUI>();
            _equipmentUI = GetComponentInChildren<PlayerEquipmentUI>();
            _inventoryUI = GetComponentInChildren<PlayerInventoryUI>();
            _factionUI = GetComponentInChildren<PlayerFactionUI>();
            _vitalityUI.Initialize();
            _interactionUI.Initialize();
        }

        public void ShowHUD()
        {
            _vitalityUI.ShowBars();
            _notificationUI.ShowNotifications();
        }

        public void HideHUD()
        {
            _vitalityUI.HideBars();
            _notificationUI.HideNotifications();
        }

        public void ShowMenu()
        {
            _menuWindow.SetActive(true);
        }

        public void HideMenu()
        {
            _menuWindow.SetActive(false);
        }
    }
}