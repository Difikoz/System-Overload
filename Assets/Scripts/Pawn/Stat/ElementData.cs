using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Element", menuName = "Winter Universe/Stat/New Element")]
    public class ElementData : ScriptableObject
    {
        public string DisplayName = "Fire";
        [TextArea] public string Description;
        public Sprite Icon;
        public StatData DamageType;
        public StatData ResistanceType;
        public StatData DamageStat;
        public StatData ResistanceStat;
        public List<GameObject> HitVFX = new();
        public List<AudioClip> HitClips = new();
    }
}