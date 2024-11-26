using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private Slider _settingsSliderMasterVolume;
        [SerializeField] private Slider _settingsSliderAmbientVolume;
        [SerializeField] private Slider _settingsSliderSoundVolume;
        [Header("Other")]
        [SerializeField] private Button _settingButtonQuitGame;

        public void Initialize()
        {
            _settingsSliderMasterVolume.onValueChanged.AddListener(OnSettingsSliderMasterVolumeChanged);
            _settingsSliderAmbientVolume.onValueChanged.AddListener(OnSettingsSliderAmbientVolumeChanged);
            _settingsSliderSoundVolume.onValueChanged.AddListener(OnSettingsSliderSoundVolumeChanged);
            _settingButtonQuitGame.onClick.AddListener(OnSettingsButtonQuitGamePressed);
            OnSettingsSliderMasterVolumeChanged(PlayerPrefs.GetFloat("MasterVolume", 1f));
            OnSettingsSliderAmbientVolumeChanged(PlayerPrefs.GetFloat("AmbientVolume", 0.5f));
            OnSettingsSliderSoundVolumeChanged(PlayerPrefs.GetFloat("SoundVolume", 0.5f));
        }

        private void OnSettingsSliderMasterVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("MasterVolume", value);
            GameManager.StaticInstance.SoundManager.SetMasterVolume(value);
        }

        private void OnSettingsSliderAmbientVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("AmbientVolume", value);
            GameManager.StaticInstance.SoundManager.SetAmbientVolume(value);
        }

        private void OnSettingsSliderSoundVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
            GameManager.StaticInstance.SoundManager.SetSoundVolume(value);
        }

        private void OnSettingsButtonQuitGamePressed()
        {
            Application.Quit();
        }
    }
}