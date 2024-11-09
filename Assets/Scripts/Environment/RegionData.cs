using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Region", menuName = "Winter Universe/Environment/New Region")]
    public class RegionData : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private List<AudioClip> _ambientClips = new();
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public List<AudioClip> AmbientClips => _ambientClips;
        public List<StatModifierCreator> Modifiers => _modifiers;
    }
}