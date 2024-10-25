using System;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public abstract class LeanAnimator
    {
        public delegate void CallBack();

        private static CallBack _callback;
        public static void ButtonOnClick(Button button , CallBack callBack, bool ignoreTimeScale = false)
        {
            var buttonTrans = button.transform;
            var initialScale = buttonTrans.localScale;
            buttonTrans.localScale = initialScale / 1.2f;
            buttonTrans.LeanScale(initialScale, 0.25f).setEaseInOutBack().setOnComplete(callBack.Invoke).setIgnoreTimeScale(ignoreTimeScale);
        }

        public static void Move(Transform uiElement, Transform destination, LeanTweenType tweenType, float duration = 1, float delay = 0, bool ignoreTimeScale = false,
            CallBack callBack = null)
        {
            var button = uiElement.GetComponent<Button>();
            if(button != null) 
                button.interactable = false;
            
            uiElement.transform.LeanMoveLocal(destination.localPosition, duration).setDelay(delay).setEase(tweenType).setOnComplete(
                () =>
                {
                    if(button != null) 
                        button.interactable = true;
                    callBack?.Invoke();
                }).setIgnoreTimeScale(ignoreTimeScale);
        }
        public static void Scale(Transform uiElement , Vector3 initialScale , Vector3 targetScale , LeanTweenType tweenType, float duration = 1 ,float delay = 0, bool ignoreTimeScale = false  , CallBack callBack = null)
        {
            uiElement.transform.localScale = initialScale;
            uiElement.transform.LeanScale(targetScale, duration).setEase(tweenType).setDelay(delay)
                .setOnComplete(() =>
                {
                    callBack?.Invoke();
                }).setIgnoreTimeScale(ignoreTimeScale);
        }
    }
}