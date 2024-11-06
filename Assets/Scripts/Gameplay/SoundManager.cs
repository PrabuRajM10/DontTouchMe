using System;
using UnityEngine;
using Enum = Enums.Enum;

namespace Gameplay
{
    public static class SoundManager
    {
        private static GameAudioData _gameAudioData;

        private static PositionalAudio _bgAudio;

        public static void Init(GameAudioData data)
        {
            _gameAudioData = data;
        }

        public static void PlaySfx(Enum.SoundType type , Vector3 position = default, bool loop = false)
        {
            var audio = ObjectPooling.Instance.GetAudioPrefab();
            PlayAudio(type, position, loop, audio);
        }
        
        public static void PlayBgMusic(bool isMainBg)
        {
            if (_bgAudio == null)
            {
                _bgAudio = ObjectPooling.Instance.GetAudioPrefab();
            }
            PlayAudio(isMainBg ? Enum.SoundType.MainBg : Enum.SoundType.GameBg, Vector3.zero, true, _bgAudio);
        }
        private static void PlayAudio(Enum.SoundType type, Vector3 position, bool loop, PositionalAudio audio)
        {
            var audioData = _gameAudioData.GetAudioByType(type);
            if (audioData != null)
                audio.SetData(audioData.Value.audioClip, audioData.Value.audioMixerGroup, position, loop);
            else
            {
                Debug.LogError("Audio data Empty for type " + type);
            }
        }
    }
}