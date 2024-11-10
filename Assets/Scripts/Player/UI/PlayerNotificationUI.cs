using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerNotificationUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _messagePrefab;
        [SerializeField] private Transform _root;

        public void ShowNotifications()
        {
            _canvasGroup.alpha = 1f;
        }

        public void HideNotifications()
        {
            _canvasGroup.alpha = 0f;
        }

        public void DisplayNotification(string message, float duration = 2f)
        {
            MessageSlotUI slot = Instantiate(_messagePrefab, _root).GetComponent<MessageSlotUI>();// TODO pool spawn
            slot.CanvasGroup.alpha = 0f;
            slot.MessageText.text = message;
            StartCoroutine(DisplayNotificationTimer(slot, duration));
        }

        private IEnumerator DisplayNotificationTimer(MessageSlotUI slot, float duration)
        {
            while (slot.CanvasGroup.alpha != 1f)
            {
                slot.CanvasGroup.alpha = Mathf.MoveTowards(slot.CanvasGroup.alpha, 1f, Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(duration);
            while (slot.CanvasGroup.alpha != 0f)
            {
                slot.CanvasGroup.alpha = Mathf.MoveTowards(slot.CanvasGroup.alpha, 0f, Time.deltaTime);
                yield return null;
            }
            Destroy(slot.gameObject);// TODO pool despawn
        }
    }
}