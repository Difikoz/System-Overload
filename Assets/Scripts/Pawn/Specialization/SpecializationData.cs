using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Specialization", menuName = "Winter Universe/Specialization/New Specialization")]
    public class SpecializationData : ScriptableObject
    {
        public string DisplayName = "Specialization";
        [TextArea] public string Description = "Description";
        public Sprite Icon;
    }
}