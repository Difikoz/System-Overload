using System.Collections;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private VitalityBarUI _healthBar;

        private PawnController _pawn;
        private Vector3 _direction;

        public void Initialize(PawnController pawn)
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
            _pawn = pawn;
            _nameText.text = _pawn.CharacterName;
            StartCoroutine(RotateBar());
            _pawn.PawnStats.OnHealthChanged += SetHealthValues;
        }

        private void SetHealthValues(float cur, float max)
        {
            _healthBar.SetValues(cur, max);
        }

        private IEnumerator RotateBar()
        {
            while (true)
            {
                _direction = transform.position - GameManager.StaticInstance.CameraManager.MainCamera.transform.position;
                _direction.y = 0f;
                _direction.Normalize();
                if (_direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(_direction);
                }
                yield return null;
            }
        }

        private void OnDestroy()
        {
            _pawn.PawnStats.OnHealthChanged -= SetHealthValues;
        }
    }
}