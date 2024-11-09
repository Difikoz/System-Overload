using UnityEngine;

namespace WinterUniverse
{
    public abstract class EffectConfig : ScriptableObject
    {
        [Header("Rework Effects to be Gameobjects\nNeed for timed effects\nDelay, Time Rate etc")]
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _value = 1;
        [SerializeField] private float _duration = 0f;
        [SerializeField] private bool _isPositive;

        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
        public float Value => _value;
        public float Duration => _duration;
        public bool IsPositive => _isPositive;

        public abstract Effect CreateEffect(PawnController pawn, PawnController source, float value, float duration);
    }
}