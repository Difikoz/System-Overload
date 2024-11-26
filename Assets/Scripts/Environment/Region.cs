using UnityEngine;

namespace WinterUniverse
{
    public class Region : EventTriggerZone
    {
        public RegionData Data;

        private WorldPlayerManager _player;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                GameManager.StaticInstance.SoundManager.ChangeAmbient(Data.AmbientClips);
                GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification($"Entered [{Data.DisplayName}]");
                foreach (StatModifierCreator creator in Data.Modifiers)
                {
                    _player.Pawn.PawnStats.AddStatModifier(creator);
                }
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                GameManager.StaticInstance.SoundManager.ChangeAmbient();
                GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification($"Quited [{Data.DisplayName}]");
                foreach (StatModifierCreator creator in Data.Modifiers)
                {
                    _player.Pawn.PawnStats.RemoveStatModifier(creator);
                }
            }
        }
    }
}