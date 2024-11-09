using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Region", menuName = "Winter Universe/Environment/New Region")]
    public class RegionData : ScriptableObject
    {
        public string DisplayName = "Mord Village";
        [TextArea] public string Description = "Description";
        public AudioClip AmbientClip;
        public List<StatModifierCreator> Modifiers = new();
    }
}