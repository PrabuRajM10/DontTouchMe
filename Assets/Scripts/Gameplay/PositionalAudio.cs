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

        public async void BackToPool()
        {
            await Task.Delay((int)audioSource.clip.length * 1000);
            _pool.AddBackToList(this , Enum.PoolObjectTypes.Audio);
        }

        public void SetData(AudioClip clip, AudioMixerGroup mixerGroup, Vector3 position = default, bool loop = false)
        {
            transform.position = position;
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.clip = clip;
            audioSource.loop = loop;
            gameObject.SetActive(true);
            audioSource.Play();
            if(!loop)BackToPool();
        }
        
        
    }
}
