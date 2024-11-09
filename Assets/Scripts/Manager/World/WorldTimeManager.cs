using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class WorldTimeManager : MonoBehaviour
    {
        public Action<int, int> OnTimeChanged;
        public Action<int, int, int> OnDateChanged;

        private bool _paused = true;

        [SerializeField] private float _timeScaleMultiplier = 600f;

        public float TimeScale = 1f;

        public float Second;
        public int Minute;
        public int Hour;
        public int Day;
        public int Month;
        public int Year;

        public void Initialize()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == 0)
            {
                _paused = true;
            }
            else
            {
                _paused = false;
            }
        }

        private void Update()
        {
            if (_paused)
            {
                return;
            }
            Second += TimeScale * _timeScaleMultiplier * Time.deltaTime;
            if (Second >= 60f)
            {
                Second -= 60f;
                AddMinute();
            }
        }

        public void AddMinute(int amount = 1)
        {
            Minute += amount;
            while (Minute >= 60)
            {
                Minute -= 60;
                AddHour();
            }
            OnTimeChanged?.Invoke(Minute, Hour);
        }

        public void AddHour(int amount = 1)
        {
            Hour += amount;
            while (Hour >= 24)
            {
                Hour -= 24;
                AddDay();
            }
            OnTimeChanged?.Invoke(Minute, Hour);
        }

        public void AddDay(int amount = 1)
        {
            Day += amount;
            while (Day > 30)
            {
                Day -= 30;
                AddMonth();
            }
            OnDateChanged?.Invoke(Day, Month, Year);
        }

        public void AddMonth(int amount = 1)
        {
            Month += amount;
            while (Month > 12)
            {
                Month -= 12;
                AddYear();
            }
            OnDateChanged?.Invoke(Day, Month, Year);
        }

        public void AddYear(int amount = 1)
        {
            Year += amount;
            OnDateChanged?.Invoke(Day, Month, Year);
        }
    }
}