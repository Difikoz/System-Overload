using UnityEngine;

namespace WinterUniverse
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private LoadingScreenUI _loadingScreenUI;
        private MainMenuUI _mainMenuUI;
        private SettingsUI _settingsUI;
        private PlayerVitalityBarUI _vitalityUI;
        private PlayerNotificationUI _notificationUI;
        private PlayerInteractionUI _interactionUI;
        private StatUI _statUI;
        private EquipmentUI _equipmentUI;
        private InventoryUI _inventoryUI;
        private FactionUI _factionUI;

        [SerializeField] private GameObject _menuWindow;

        public LoadingScreenUI LoadingScreenUI => _loadingScreenUI;
        public PlayerNotificationUI NotificationUI => _notificationUI;

        public void Initialize()
        {
            //_loadingScreen = GetComponentInChildren<LoadingScreenUI>();
            _mainMenuUI = GetComponentInChildren<MainMenuUI>();
            _settingsUI = GetComponentInChildren<SettingsUI>();
            _vitalityUI = GetComponentInChildren<PlayerVitalityBarUI>();
            _notificationUI = GetComponentInChildren<PlayerNotificationUI>();
            _interactionUI = GetComponentInChildren<PlayerInteractionUI>();
            _statUI = GetComponentInChildren<StatUI>();
            _equipmentUI = GetComponentInChildren<EquipmentUI>();
            _inventoryUI = GetComponentInChildren<InventoryUI>();
            _factionUI = GetComponentInChildren<FactionUI>();
            _mainMenuUI.Initialize();
            _settingsUI.Initialize();
            _vitalityUI.Initialize();
            _interactionUI.Initialize();
            _statUI.Initialize();
            _equipmentUI.Initialize();
            _inventoryUI.Initialize();
            _factionUI.Initialize();
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