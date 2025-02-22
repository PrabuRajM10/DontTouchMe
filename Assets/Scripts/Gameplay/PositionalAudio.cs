using System.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay
{
    [RequireComponent(typeof(AudioSource))]
    public class PositionalAudio : MonoBehaviour , IPoolableObjects
    {
        [SerializeField] private AudioSource audioSource;
        private ObjectPooling _pool;

        public void Init(ObjectPooling pool)
        {
            _pool = pool;
            if(audioSource == null)audioSource = GetComponent<AudioSource>();
        }

        public void BackToPool()
        {
            _pool.AddBackToList(this , DTMEnum.PoolObjectTypes.Audio);
        }

        public void SetData(AudioClip clip, AudioMixerGroup mixerGroup, Vector3 position = default, bool loop = false)
        {
            transform.position = position;
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.clip = clip;
            audioSource.loop = loop;
            gameObject.SetActive(true);
            audioSource.Play();
            if(!loop)Invoke(nameof(BackToPool) , audioSource.clip.length);
        }
        
        
    }
}
