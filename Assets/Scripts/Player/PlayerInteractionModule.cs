using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteractionModule : InteractionModule
    {
        private PlayerController _player;

        protected override void Awake()
        {
            base.Awake();
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