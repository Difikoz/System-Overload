using System;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldTimeManager : MonoBehaviour
    {
        public Action<int, int> OnTimeChanged;
        public Action<int, int, int> OnDateChanged;

        private bool _paused = true;

        [SerializeField] private float _timeScaleMultiplier = 600f;
        [SerializeField] private float _timeScale = 1f;
        [SerializeField] private float _second;
        [SerializeField] private int _minute;
        [SerializeField] private int _hour;
        [SerializeField] private int _day;
        [SerializeField] private int _month;
        [SerializeField] private int _year;

        public float TimeScale => _timeScale;
        public float Second => _second;
        public int Minute => _minute;
        public int Hour => _hour;
        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
        public bool Paused => _paused;

        private void Update()
        {
            if (_paused)
            {
                return;
            }
            _second += TimeScale * _timeScaleMultiplier * Time.deltaTime;
            if (_second >= 60f)
            {
                _second -= 60f;
                AddMinute();
            }
        }

        public void PauseGame()
        {
            _paused = true;
            // other logic
        }

        public void UpauseGame()
        {
            _paused = false;
            // other logic
        }

        public void AddMinute(int amount = 1)
        {
            _minute += amount;
            while (_minute >= 60)
            {
                _minute -= 60;
                AddHour();
            }
            OnTimeChanged?.Invoke(_minute, _hour);
        }

        public void AddHour(int amount = 1)
        {
            _hour += amount;
            while (_hour >= 24)
            {
                _hour -= 24;
                AddDay();
            }
            OnTimeChanged?.Invoke(_minute, _hour);
        }

        public void AddDay(int amount = 1)
        {
            _day += amount;
            while (_day > 30)
            {
                _day -= 30;
                AddMonth();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddMonth(int amount = 1)
        {
            _month += amount;
            while (_month > 12)
            {
                _month -= 12;
                AddYear();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddYear(int amount = 1)
        {
            _year += amount;
            OnDateChanged?.Invoke(_day, _month, _year);
        }
    }
}