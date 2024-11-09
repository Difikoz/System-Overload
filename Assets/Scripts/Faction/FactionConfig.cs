using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Winter Universe/Faction/New Faction")]
    public class FactionConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Sprite _icon;
        [SerializeField] private List<FactionRelationship> _relationships = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public List<FactionRelationship> Relationships => _relationships;

        public RelationshipState GetState(FactionConfig other)
        {
            foreach (FactionRelationship relationship in _relationships)
            {
                if (relationship.Faction == other)
                {
                    return relationship.State;
                }
            }
            Debug.LogError($"[{_displayName}] dont have relation with [{other.DisplayName}]. Fix it! Just now returned as [Neutral]!");
            return RelationshipState.Neutral;
        }
    }
}