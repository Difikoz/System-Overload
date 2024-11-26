using UnityEngine;

namespace WinterUniverse
{
    public class PlayerVitalityUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;

        public void Initialize()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.PawnStats.OnHealthChanged += SetHealthValues;
            GameManager.StaticInstance.PlayerManager.Pawn.PawnStats.OnEnergyChanged += SetEnergyValues;
        }

        public void ShowBars()
        {
            _canvasGroup.alpha = 1f;
        }

        public void HideBars()
        {
            _canvasGroup.alpha = 0f;
        }

        private void SetHealthValues(float cur, float max)
        {
            _healthBar.SetValues(cur, max);
        }

        private void SetEnergyValues(float cur, float max)
        {
            _energyBar.SetValues(cur, max);
        }

        private void OnDestroy()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.PawnStats.OnHealthChanged -= SetHealthValues;
            GameManager.StaticInstance.PlayerManager.Pawn.PawnStats.OnEnergyChanged -= SetEnergyValues;
        }
    }
}