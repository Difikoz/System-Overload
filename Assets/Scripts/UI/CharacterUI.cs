using System.Collections;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private float _hideBarTime = 10f;

        private Vector3 _direction;
        private Coroutine _rotateCoroutine;
        private Coroutine _hideBarCoroutine;

        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
            HideBar();
        }

        public void ShowBar()
        {
            _canvasGroup.alpha = 1f;
            if (_hideBarCoroutine != null)
            {
                StopCoroutine(_hideBarCoroutine);
            }
            _hideBarCoroutine = StartCoroutine(HideBarTimer());
            if (_rotateCoroutine != null)
            {
                StopCoroutine(_rotateCoroutine);
            }
            _rotateCoroutine = StartCoroutine(RotateBar());
        }

        public void HideBar()
        {
            if (_rotateCoroutine != null)
            {
                StopCoroutine(_rotateCoroutine);
                _rotateCoroutine = null;
            }
            _canvasGroup.alpha = 0f;
        }

        public void SetCharacterName(string name)
        {
            _nameText.text = name;
        }

        public void SetHealthValues(float cur, float max)
        {
            ShowBar();
            _healthBar.SetValues(cur, max);
        }

        private IEnumerator RotateBar()
        {
            while (true)
            {
                _direction = transform.position - GameManager.StaticInstance.PlayerCamera.Camera.transform.position;
                _direction.y = 0f;
                _direction.Normalize();
                if (_direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(_direction);
                }
                yield return null;
            }
        }

        private IEnumerator HideBarTimer()
        {
            yield return new WaitForSeconds(_hideBarTime);
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
            HideBar();
            _hideBarCoroutine = null;
        }
    }
}