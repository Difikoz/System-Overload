using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Winter Universe/Stat/New Stat")]
    public class StatConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Sprite _icon;
        //[SerializeField] private float _baseValue;
        [SerializeField] private bool _clampMinValue;
        [SerializeField] private float _minValue;
        [SerializeField] private bool _clampMaxValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private bool _isPercent;

        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        //public float BaseValue => _baseValue;
        public bool ClampMinValue => _clampMinValue;
        public float MinValue => _minValue;
        public bool ClampMaxValue => _clampMaxValue;
        public float MaxValue => _maxValue;
        public bool IsPercent => _isPercent;
    }
}