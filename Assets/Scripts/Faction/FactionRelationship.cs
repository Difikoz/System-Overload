using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class FactionRelationship
    {
        [SerializeField] private FactionConfig _faction;
        [SerializeField] private RelationshipState _state;

        public FactionConfig Faction => _faction;
        public RelationshipState State => _state;

        public FactionRelationship(FactionConfig faction, RelationshipState state)
        {
            _faction = faction;
            _state = state;
        }
    }
}