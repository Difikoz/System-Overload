using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatModifierCreator
    {
        [SerializeField] private StatConfig _stat;
        [SerializeField] private StatModifier _modifier;

        public StatConfig Stat => _stat;
        public StatModifier Modifier => _modifier;

        public StatModifierCreator(StatConfig stat, StatModifier modifier)
        {
            _stat = stat;
            _modifier = modifier;
        }
    }
}