using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class LoadingScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreenWindow;
        [SerializeField] private Slider _loadingScreenSliderProgress;
        [SerializeField] private TMP_Text _loadingScreenTextProgress;

        public void Show()
        {
            _loadingScreenWindow.SetActive(true);
        }

        public void Hide()
        {
            _loadingScreenWindow.SetActive(false);
        }

        public void UpdateLoadingScreen(string message, int currentValue, int maxValue)
        {
            _loadingScreenSliderProgress.value = currentValue;
            _loadingScreenSliderProgress.maxValue = maxValue;
            _loadingScreenTextProgress.text = $"{message} : {currentValue}/{maxValue}";
        }
    }
}