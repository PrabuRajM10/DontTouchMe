using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ImageFillLoader : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private bool isReverse;

        public void StartLoading(float time)
        {
            gameObject.SetActive(true);
            StartCoroutine(FillImage(time));
        }

        IEnumerator FillImage(float fillTime)
        {
            var totalTime = fillTime;
            float currentTime = 0;
            
            while (currentTime <= totalTime)
            {
                fillImage.fillAmount = (currentTime / totalTime);
                currentTime += Time.deltaTime;
                yield return null;
            }
            // if (isReverse)
            // {
            //     // var totalTime = 0;
            //     // float currentTime = fillTime;
            //     //
            //     // while (currentTime >= totalTime)
            //     // {
            //     //     fillImage.fillAmount = (totalTime / currentTime );
            //     //     currentTime -= Time.deltaTime;
            //     //     yield return null;
            //     // }
            //     //
            //     var totalTime = fillTime;
            //     float currentTime = 0;
            //     
            //     while (currentTime <= totalTime)
            //     {
            //         fillImage.fillAmount = (currentTime / totalTime) - 1;
            //         currentTime += Time.deltaTime;
            //         yield return null;
            //     }
            // }
            // else
            // {
            // }

            OnTimerDone();
        }

        void OnTimerDone()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            fillImage.fillAmount = 0;
            OnTimerDone();
        }
    }
}