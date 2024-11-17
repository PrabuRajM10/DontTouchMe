using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace Helpers
{
    public class CameraShake : MonoBehaviour
    {
        private static CinemachineVirtualCamera _virtualCamera;
        private static CinemachineBasicMultiChannelPerlin _noise;
        private static float _shakeTimer;

        public static void Init(CinemachineVirtualCamera virtualCamera)
        {
            _virtualCamera = virtualCamera;
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _noise.m_AmplitudeGain = 0;
        }

        public static void ShakeCamera(float intensity = 3f ,float duration = 0.2f)
        {
            _noise.m_AmplitudeGain = 1;
            _noise.m_FrequencyGain = intensity;
            _shakeTimer = duration;
        }
        
        void Update()
        {
            if (!(_shakeTimer > 0)) return;
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {
                _noise.m_AmplitudeGain = 0;
            }
        }
    }
}