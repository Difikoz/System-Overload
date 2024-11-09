using UnityEngine;

namespace WinterUniverse
{
    public class PlayerHUD : MonoBehaviour
    {
        [HideInInspector] public PlayerVitalityBarUI VitalityUI;
        [HideInInspector] public PlayerNotificationUI NotificationUI;
        [HideInInspector] public PlayerInteractionUI InteractionUI;

        private void Awake()
        {
            VitalityUI = GetComponentInChildren<PlayerVitalityBarUI>();
            NotificationUI = GetComponentInChildren<PlayerNotificationUI>();
            InteractionUI = GetComponentInChildren<PlayerInteractionUI>();
        }

        public void ShowHud()
        {
            VitalityUI.ShowBars();
            NotificationUI.ShowNotifications();
        }

        public void HideHUD()
        {
            VitalityUI.HideBars();
            NotificationUI.HideNotifications();
            InteractionUI.HideMessage();
        }
    }
}