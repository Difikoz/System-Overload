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
            _menuUI = GetComponentInChildren<MenuUI>(true);
            _settingsUI = GetComponentInChildren<SettingsUI>(true);
            _statUI = GetComponentInChildren<StatUI>(true);
            _equipmentUI = GetComponentInChildren<EquipmentUI>(true);
            _inventoryUI = GetComponentInChildren<InventoryUI>(true);
            _factionUI = GetComponentInChildren<FactionUI>(true);
            _vitalityUI = GetComponentInChildren<PlayerVitalityUI>(true);
            _notificationUI = GetComponentInChildren<PlayerNotificationUI>(true);
            _interactionUI = GetComponentInChildren<PlayerInteractionUI>(true);
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