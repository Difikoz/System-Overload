using UnityEngine;

namespace WinterUniverse
{
    public class PlayerUIManager : MonoBehaviour
    {
        public PlayerHUD HUD;
        public PlayerMenuUI MenuUI;

        public void Initialize()
        {
            HUD = GetComponentInChildren<PlayerHUD>();
            MenuUI = GetComponentInChildren<PlayerMenuUI>();
        }

        public void ShowUI()
        {
            HUD.ShowHud();
        }

        public void HideUI()
        {
            HUD.HideHUD();
        }
    }
}