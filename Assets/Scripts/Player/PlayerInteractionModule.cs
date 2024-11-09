using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteractionModule : PawnInteraction
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }

        public override void RefreshInteractables()
        {
            base.RefreshInteractables();
            if (_interactables.Count > 0)
            {
                PlayerUIManager.StaticInstance.HUD.InteractionUI.DisplayMessage(_interactables[0].GetInteractionMessage());
            }
            else
            {
                PlayerUIManager.StaticInstance.HUD.InteractionUI.HideMessage();
            }
        }
    }
}