using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteractionUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _messageText;

        public void Initialize()
        {
            GameManager.StaticInstance.Player.PawnInteraction.OnRefreshInteractables += OnRefreshInteractables;
        }

        private void OnRefreshInteractables(List<Interactable> interactables)
        {
            if(interactables.Count > 0)
            {
                _messageText.text = interactables[0].GetInteractionMessage();
                _canvasGroup.alpha = 1f;
            }
            else
            {
                _canvasGroup.alpha = 0f;
            }
        }

        private void OnDestroy()
        {
            GameManager.StaticInstance.Player.PawnInteraction.OnRefreshInteractables -= OnRefreshInteractables;
        }
    }
}