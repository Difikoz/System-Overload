using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Race", menuName = "Winter Universe/Race/New Race")]
    public class RaceConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _model;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public GameObject Model => _model;
        public List<StatModifierCreator> Modifiers => _modifiers;
    }
}