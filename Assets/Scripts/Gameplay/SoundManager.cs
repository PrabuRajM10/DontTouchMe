using System;
using Enums;
using UnityEngine;

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
        public static void PlaySound(DTMEnum.SoundType type, Vector3 position = default, bool loop = false)
        {
            var audio = ObjectPooling.Instance.GetAudioPrefab();
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