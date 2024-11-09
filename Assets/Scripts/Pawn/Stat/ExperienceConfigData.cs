using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Experience Config", menuName = "Winter Universe/Stat/New Experience Config")]
    public class ExperienceConfigData : ScriptableObject
    {
        public int MaxLevel = 50;
        public int MaxExperience = 100000;
        public AnimationCurve LevelCurve;
        public List<int> Levels = new();

        public int GetRequiredExperience(int level)
        {
            return Mathf.FloorToInt(LevelCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level + 1)) * MaxExperience);
        }

        private void OnValidate()
        {
            Levels.Clear();
            for (int i = 0; i < MaxLevel; i++)
            {
                Levels.Add(GetRequiredExperience(i));
            }
        }
    }
}