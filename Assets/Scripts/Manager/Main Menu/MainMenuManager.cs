using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Loading Screen")]
        [SerializeField] private GameObject _loadingScreenWindow;
        [SerializeField] private Slider _loadingScreenSliderProgress;
        [SerializeField] private TMP_Text _loadingScreenTextProgress;
        [Header("Main Menu")]
        [SerializeField] private GameObject _mainMenuWindow;
        [SerializeField] private Button _mainMenuButtonContinue;
        [SerializeField] private Button _mainMenuButtonSettings;
        [SerializeField] private Button _mainMenuButtonQuitGame;
        [Header("Settings")]
        [SerializeField] private GameObject _settingsWindow;
        [SerializeField] private Slider _settingsSliderMasterVolume;
        [SerializeField] private Slider _settingsSliderAmbientVolume;
        [SerializeField] private Slider _settingsSliderSoundVolume;
        [SerializeField] private Button _settingsButtonBackToMainMenu;

        public void Initialize()
        {
            _loadingScreenWindow.SetActive(true);
            _mainMenuButtonContinue.onClick.AddListener(OnMainMenuButtonContinuePressed);
            _mainMenuButtonSettings.onClick.AddListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.AddListener(OnainMenuButtonQuitGamePressed);
            _settingsSliderMasterVolume.onValueChanged.AddListener(OnSettingsSliderMasterVolumeChanged);
            _settingsSliderAmbientVolume.onValueChanged.AddListener(OnSettingsSliderAmbientVolumeChanged);
            _settingsSliderSoundVolume.onValueChanged.AddListener(OnSettingsSliderSoundVolumeChanged);
            _settingsButtonBackToMainMenu.onClick.AddListener(OnSettingsMenuButtonBackToMainMenuPressed);
            OnSettingsSliderMasterVolumeChanged(PlayerPrefs.GetFloat("MasterVolume", 1f));
            OnSettingsSliderAmbientVolumeChanged(PlayerPrefs.GetFloat("AmbientVolume", 0.5f));
            OnSettingsSliderSoundVolumeChanged(PlayerPrefs.GetFloat("SoundVolume", 0.5f));
        }

        public void UpdateLoadingScreen(string message, int currentValue, int maxValue)
        {
            _loadingScreenSliderProgress.value = currentValue;
            _loadingScreenSliderProgress.maxValue = maxValue;
            _loadingScreenTextProgress.text = $"{message} : {currentValue}/{maxValue}";
        }

        public void OpenMainMenuWindow()
        {
            _loadingScreenWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonContinue.Select();
        }

        private void OnMainMenuButtonContinuePressed()
        {
            _mainMenuWindow.SetActive(false);
        }

        private void OnMainMenuButtonSettingsPressed()
        {
            _settingsWindow.SetActive(true);
            _settingsButtonBackToMainMenu.Select();
        }

        private void OnainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }

        private void OnSettingsSliderMasterVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("MasterVolume", value);
            GameManager.StaticInstance.WorldSound.SetMasterVolume(value);
        }

        private void OnSettingsSliderAmbientVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("AmbientVolume", value);
            GameManager.StaticInstance.WorldSound.SetAmbientVolume(value);
        }

        private void OnSettingsSliderSoundVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
            GameManager.StaticInstance.WorldSound.SetSoundVolume(value);
        }

        private void OnSettingsMenuButtonBackToMainMenuPressed()
        {
            _settingsWindow.SetActive(false);
            _mainMenuButtonSettings.Select();
        }
    }
}