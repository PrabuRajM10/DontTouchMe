using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Enum = Enums.Enum;

namespace Gameplay
{
    [Serializable]
    public class AudioData
    {
        public Enum.SoundType audioType;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
    }
    
    [CreateAssetMenu(menuName = "Create AudioData", fileName = "AudioData", order = 0)]
    public class GameAudioData : ScriptableObject
    {
        [SerializeField] private List<AudioData> _audioDatas = new List<AudioData>();

        public (AudioClip audioClip, AudioMixerGroup audioMixerGroup)? GetAudioByType(Enum.SoundType type)
        {
            foreach (var audioData in _audioDatas.Where(audioData => audioData.audioType == type))
            {
                return new(audioData.audioClip, audioData.audioMixerGroup);
            }

            return null;
        }
    }
}
