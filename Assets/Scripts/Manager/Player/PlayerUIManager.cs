using UnityEngine;

namespace WinterUniverse
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private LoadingScreenUI _loadingScreenUI;
        private MenuUI _menuUI;
        private SettingsUI _settingsUI;
        private StatUI _statUI;
        private EquipmentUI _equipmentUI;
        private InventoryUI _inventoryUI;
        private FactionUI _factionUI;
        private PlayerVitalityUI _vitalityUI;
        private PlayerNotificationUI _notificationUI;
        private PlayerInteractionUI _interactionUI;

        public LoadingScreenUI LoadingScreenUI => _loadingScreenUI;
        public PlayerNotificationUI NotificationUI => _notificationUI;
        public MenuUI MenuUI => _menuUI;

        public void Initialize()
        {
            //_loadingScreen = GetComponentInChildren<LoadingScreenUI>();
            _menuUI = GetComponentInChildren<MenuUI>();
            _settingsUI = GetComponentInChildren<SettingsUI>();
            _statUI = GetComponentInChildren<StatUI>();
            _equipmentUI = GetComponentInChildren<EquipmentUI>();
            _inventoryUI = GetComponentInChildren<InventoryUI>();
            _factionUI = GetComponentInChildren<FactionUI>();
            _vitalityUI = GetComponentInChildren<PlayerVitalityUI>();
            _notificationUI = GetComponentInChildren<PlayerNotificationUI>();
            _interactionUI = GetComponentInChildren<PlayerInteractionUI>();
            _menuUI.Initialize();
            _settingsUI.Initialize();
            _statUI.Initialize();
            _equipmentUI.Initialize();
            _inventoryUI.Initialize();
            _factionUI.Initialize();
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
    }
}