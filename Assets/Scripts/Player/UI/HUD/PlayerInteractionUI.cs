using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteractionUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _messageText;

        public void DisplayMessage(string message)
        {
            _messageText.text = message;
            _canvasGroup.alpha = 1f;
        }

        public void HideMessage()
        {
            _canvasGroup.alpha = 0f;
        }
    }
}