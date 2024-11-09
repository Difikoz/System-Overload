using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Experience Config", menuName = "Winter Universe/Stat/New Experience Config")]
    public class ExperienceConfig : ScriptableObject
    {
        [SerializeField] private int _maxLevel = 50;
        [SerializeField] private int _maxExperience = 100000;
        [SerializeField] private AnimationCurve _levelCurve;
        [SerializeField] private List<int> _levels = new();

        public int MaxLevel => _maxLevel;
        public int MaxExperience => _maxExperience;
        public AnimationCurve LevelCurve => _levelCurve;
        public List<int> Levels => _levels;

        public int GetRequiredExperience(int level)
        {
            return Mathf.FloorToInt(_levelCurve.Evaluate(Mathf.InverseLerp(0, _maxLevel, level + 1)) * _maxExperience);
        }

        private void OnValidate()
        {
            _levels.Clear();
            for (int i = 0; i < _maxLevel; i++)
            {
                _levels.Add(GetRequiredExperience(i));
            }
        }
    }
}