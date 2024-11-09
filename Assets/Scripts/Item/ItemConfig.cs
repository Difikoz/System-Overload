using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ItemConfig : ScriptableObject
    {
        [Header("Basic Information")]
        [SerializeField] protected string _displayName = "Name";
        [SerializeField, TextArea] protected string _description = "Description";
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected GameObject _model;
        [SerializeField] protected float _weight = 1f;
        [SerializeField] protected int _maxCountInStack = 1;
        [SerializeField] protected int _price = 100;
        [SerializeField] protected float _rating = 1f;
        [Header("Requirements for usable items")]
        [SerializeField] protected int _requiredLevel;
        [SerializeField] protected List<RaceConfig> _requiredRace = new();
        [SerializeField] protected List<FactionConfig> _requiredFaction = new();
        [SerializeField] protected List<StatRequirement> _requiredStats = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public ItemType ItemType => _itemType;
        public GameObject Model => _model;
        public float Weight => _weight;
        public int MaxCountInStack => _maxCountInStack;
        public int Price => _price;
        public float Rating => _rating;
        public int RequiredLevel => _requiredLevel;
        public List<RaceConfig> RequiredRace => _requiredRace;
        public List<FactionConfig> RequiredFaction => _requiredFaction;
        public List<StatRequirement> RequiredStats => _requiredStats;

        public virtual bool CanUse(PawnController character, out string error, bool fromInventory = true)
        {
            error = string.Empty;
            if (character.PawnStats.Level < _requiredLevel)
            {
                error = "Low Level";
                return false;
            }
            if (_requiredRace.Count > 0)
            {
                if (!_requiredRace.Contains(character.Race))
                {
                    error = "Wrong Race";
                    return false;
                }
            }
            if (_requiredFaction.Count > 0)
            {
                if (!_requiredFaction.Contains(character.Faction))
                {
                    error = "Wrong Faction";
                    return false;
                }
            }
            if (_requiredStats.Count > 0)
            {
                foreach (StatRequirement requirement in _requiredStats)
                {
                    if (requirement.Type == RequirementType.GreaterOrEqual)
                    {
                        if (character.PawnStats.GetStatByName(requirement.Stat.DisplayName).CurrentValue < requirement.Value)
                        {
                            error = $"Low {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                    else if (requirement.Type == RequirementType.LessOrEqual)
                    {
                        if (character.PawnStats.GetStatByName(requirement.Stat.DisplayName).CurrentValue > requirement.Value)
                        {
                            error = $"High {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                    else if (requirement.Type == RequirementType.Equal)
                    {
                        if (character.PawnStats.GetStatByName(requirement.Stat.DisplayName).CurrentValue != requirement.Value)
                        {
                            error = $"Not equal {requirement.Stat.DisplayName}. Need {requirement.Value}";
                            return false;
                        }
                    }
                }
            }
            return !fromInventory || character.PawnInventory.AmountOfItem(this) > 0;
        }

        public virtual void Use(PawnController character, bool fromInventory = true)
        {

        }
    }
}