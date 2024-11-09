using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Winter Universe/Faction/New Faction")]
    public class FactionData : ScriptableObject
    {
        public string DisplayName = "Faction";
        [TextArea] public string Description = "Description";
        public Sprite Icon;
        public List<FactionRelationship> Relationships = new();

        public RelationshipState GetState(FactionData other)
        {
            foreach (FactionRelationship relationship in Relationships)
            {
                if (relationship.Faction == other)
                {
                    return relationship.State;
                }
            }
            Debug.LogError($"[{DisplayName}] dont have relation with [{other.DisplayName}]. Fix it! Just now returned as [Neutral]!");
            return RelationshipState.Neutral;
        }
    }

    [System.Serializable]
    public class FactionRelationship
    {
        public FactionData Faction;
        public RelationshipState State;

        public FactionRelationship(FactionData faction, RelationshipState state)
        {
            Faction = faction;
            State = state;
        }
    }
}