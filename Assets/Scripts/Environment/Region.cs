using UnityEngine;

namespace WinterUniverse
{
    public class Region : EventTriggerZone
    {
        public RegionData Data;

        private PlayerController _player;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                GameManager.StaticInstance.WorldSound.ChangeAmbient(Data.AmbientClips);
                GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification($"Entered [{Data.DisplayName}]");
                foreach (StatModifierCreator creator in Data.Modifiers)
                {
                    _player.PawnStats.AddStatModifier(creator);
                }
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                GameManager.StaticInstance.WorldSound.ChangeAmbient();
                GameManager.StaticInstance.PlayerUI.NotificationUI.DisplayNotification($"Quited [{Data.DisplayName}]");
                foreach (StatModifierCreator creator in Data.Modifiers)
                {
                    _player.PawnStats.RemoveStatModifier(creator);
                }
            }
        }
    }
}