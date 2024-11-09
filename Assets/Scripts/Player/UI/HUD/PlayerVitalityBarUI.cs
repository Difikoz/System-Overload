using UnityEngine;

namespace WinterUniverse
{
    public class PlayerVitalityBarUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;

        public void ShowBars()
        {
            _canvasGroup.alpha = 1f;
        }

        public void HideBars()
        {
            _canvasGroup.alpha = 0f;
        }

        public void SetHealthValues(float cur, float max)
        {
            _healthBar.SetValues(cur, max);
        }

        public void SetEnergyValues(float cur, float max)
        {
            _energyBar.SetValues(cur, max);
        }
    }
}