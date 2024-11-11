using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Slider _settingsSliderMasterVolume;
        [SerializeField] private Slider _settingsSliderAmbientVolume;
        [SerializeField] private Slider _settingsSliderSoundVolume;

        public void Initialize()
        {
            _settingsSliderMasterVolume.onValueChanged.AddListener(OnSettingsSliderMasterVolumeChanged);
            _settingsSliderAmbientVolume.onValueChanged.AddListener(OnSettingsSliderAmbientVolumeChanged);
            _settingsSliderSoundVolume.onValueChanged.AddListener(OnSettingsSliderSoundVolumeChanged);
            OnSettingsSliderMasterVolumeChanged(PlayerPrefs.GetFloat("MasterVolume", 1f));
            OnSettingsSliderAmbientVolumeChanged(PlayerPrefs.GetFloat("AmbientVolume", 0.5f));
            OnSettingsSliderSoundVolumeChanged(PlayerPrefs.GetFloat("SoundVolume", 0.5f));
            gameObject.SetActive(false);
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
    }
}