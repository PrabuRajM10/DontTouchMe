using UnityEngine;

namespace PlayerPrefs
{
    public static class PlayerPrefManager
    {
        private static int IsCoinCollectedForFirstTime => UnityEngine.PlayerPrefs.GetInt(PlayerPrefIds.IsCoinCollectedForFirstTime);
        private static int IsSpellCollectedForFirstTime => UnityEngine.PlayerPrefs.GetInt(PlayerPrefIds.IsSpellCollectedForFirstTime);
        private static int GotKillForFirstTime => UnityEngine.PlayerPrefs.GetInt(PlayerPrefIds.GotKillForFirstTime);

        public static int GetIsCoinCollectedForFirstTime()
        {
            return IsCoinCollectedForFirstTime;
        }

        public static void UpdateIsCoinCollectedForFirstTime(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(PlayerPrefIds.IsCoinCollectedForFirstTime , value);
        }
        public static int GetIsSpellCollectedForFirstTime()
        {
            return IsSpellCollectedForFirstTime;
        }

        public static void UpdateIsSpellCollectedForFirstTime(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(PlayerPrefIds.IsSpellCollectedForFirstTime , value);
        }
        
        public static int GetGotKillForFirstTime()
        {
            return GotKillForFirstTime;
        }

        public static void UpdateGotKillForFirstTime(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(PlayerPrefIds.GotKillForFirstTime , value);
        }
    }
}
