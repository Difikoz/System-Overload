using System.Collections;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class VitalityBarUI : MonoBehaviour
    {
        [SerializeField] private Image _backgroundFill;
        [SerializeField] private Image _foregroundFill;
        [SerializeField] private Color _reduceColor;
        [SerializeField] private Color _restoreColor;
        //[SerializeField] private TextMeshProUGUI _valueText;

        private float _floatValue;
        private float _currentValue;
        private float _maxValue;
        private float _differenceValue;
        private Coroutine _coroutine;

        public void SetValues(float cur, float max)
        {
            _currentValue = cur;
            _maxValue = max;
            //_valueText.text = $"{_currentValue:0}/{_maxValue:0}";
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(ChangeValueTimer());
        }

        private IEnumerator ChangeValueTimer()
        {
            UpdateUI();
            yield return null;
            while (_differenceValue != 0f)
            {
                _floatValue = Mathf.MoveTowards(_floatValue, _currentValue, (_differenceValue + 1f) * Time.deltaTime);
                UpdateUI();
                yield return null;
            }
            _coroutine = null;
        }

        private void UpdateUI()
        {
            _differenceValue = 0f;
            if (_currentValue > _floatValue)// restore
            {
                _differenceValue = _currentValue - _floatValue;
                _backgroundFill.color = _restoreColor;
                _backgroundFill.fillAmount = _currentValue / _maxValue;
                _foregroundFill.fillAmount = _floatValue / _maxValue;
            }
            else if (_floatValue > _currentValue)// reduce
            {
                _differenceValue = _floatValue - _currentValue;
                _backgroundFill.color = _reduceColor;
                _backgroundFill.fillAmount = _floatValue / _maxValue;
                _foregroundFill.fillAmount = _currentValue / _maxValue;
            }
            else
            {
                _backgroundFill.color = Color.black;
                _backgroundFill.fillAmount = _floatValue / _maxValue;
                _foregroundFill.fillAmount = _floatValue / _maxValue;
            }
        }
    }
}