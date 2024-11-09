using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Main Menu")]
        [SerializeField] private GameObject _mainMenuWindow;
        [SerializeField] private Button _mainMenuButtonContinue;
        [SerializeField] private Button _mainMenuButtonSettings;
        [SerializeField] private Button _mainMenuButtonQuitGame;
        [Header("Settings Menu")]
        [SerializeField] private GameObject _settingsMenuWindow;
        [SerializeField] private Button _settingsMenuButtonBackToMainMenu;

        public void Initialize()
        {
            _mainMenuButtonContinue.onClick.AddListener(OnMainMenuButtonContinuePressed);
            _mainMenuButtonSettings.onClick.AddListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.AddListener(OnainMenuButtonQuitGamePressed);
            _settingsMenuButtonBackToMainMenu.onClick.AddListener(OnSettingsMenuButtonBackToMainMenuPressed);
        }

        public void OpenMainMenuWindow()
        {
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonContinue.Select();
        }

        private void OnMainMenuButtonContinuePressed()
        {
            _mainMenuWindow.SetActive(false);
        }

        private void OnMainMenuButtonSettingsPressed()
        {
            _settingsMenuWindow.SetActive(true);
            _settingsMenuButtonBackToMainMenu.Select();
        }

        private void OnainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }

        private void OnSettingsMenuButtonBackToMainMenuPressed()
        {
            _settingsMenuWindow.SetActive(false);
            _mainMenuButtonSettings.Select();
        }
    }
}