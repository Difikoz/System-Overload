using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Race", menuName = "Winter Universe/Race/New Race")]
    public class RaceData : ScriptableObject
    {
        public string DisplayName = "Human";
        [TextArea] public string Description = "Description";
        public Sprite Icon;
        public GameObject Model;
        public List<StatModifierCreator> Modifiers = new();
    }
}