using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Element", menuName = "Winter Universe/Stat/New Element")]
    public class ElementConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatConfig _damageStat;
        [SerializeField] private StatConfig _resistanceStat;
        [SerializeField] private List<GameObject> _hitVFX = new();
        [SerializeField] private List<AudioClip> _hitClips = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public StatConfig DamageStat => _damageStat;
        public StatConfig ResistanceStat => _resistanceStat;
        public List<GameObject> HitVFX => _hitVFX;
        public List<AudioClip> HitClips => _hitClips;
    }
}