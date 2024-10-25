using System.Collections;
using TMPro;
using UnityEngine;

namespace Helpers
{
    public static class Utils
    {
        
        public delegate void Callback();

        private static Callback _callback;
        private static float _currentTimer;

        public static float GetValueByPercentage(float min , float max , float percentage)
        {
            return min + (percentage / 100) * (max - min);
        }
        
        
        public static Vector2 GetRandomPointAroundTarget(Vector3 targetPos , float scale)
        {
            return Random.insideUnitCircle * scale + new Vector2(targetPos.x, targetPos.z);
        }
        
        public static IEnumerator TimerDisplay(float maxTimer, Callback onTimerDone)
        {
            Debug.Log("[Utils] [StartTimer] called maxTimer " + maxTimer);
            _currentTimer = maxTimer;

            while (_currentTimer > 0)
            {
                _currentTimer--;

                yield return new WaitForSeconds(1);
            }
            
            onTimerDone?.Invoke();
        }

        public static float GetCurrentTime()
        {
            return _currentTimer;
        }

        public static string GetTimeString()
        {
            var minutes = Mathf.FloorToInt(_currentTimer / 60);
            var seconds = Mathf.FloorToInt(_currentTimer % 60);

            return $"{minutes:00}:{seconds:00}";
        }

        public static void QuitGame()
        {
            PopUp.ShowPopUp("Attention" , "Do you really want to quit game" , PopUpType.YES_NO , () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else           
            Application.Quit();
#endif
            }, PopUp.ClosePopUp);


        }
    }
}