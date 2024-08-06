using System;
using Helpers;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public static class GameTimer
    {
        private static float _maxTime;
        private static float _currentTime;
        private static MonoBehaviour _mono;
        private static Coroutine _timer;

        public static event Action OnGameTimerDone; 

        public static void StartTimer(MonoBehaviour mono , float maxTime)
        {
            _mono = mono;
            _maxTime = maxTime;
            _timer = _mono.StartCoroutine(Utils.TimerDisplay(_maxTime, OnTimerDone));
        }

        private static void OnTimerDone()
        {
            OnGameTimerDone?.Invoke();
        }

        public static void StopTimer()
        {
            _mono.StopCoroutine(_timer);
        }

        public static float GetCurrentTime()
        {
            return Utils.GetCurrentTime();
        }

        public static string GetTimeString()
        {
            return Utils.GetTimeString();
        }
    }
}