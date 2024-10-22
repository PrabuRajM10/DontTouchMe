using System;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public abstract class UiAnimator
    {
        public delegate void CallBack();

        private static CallBack _callback;
        public static void ButtonOnClick(Button button , CallBack callBack)
        {
            var buttonTrans = button.transform;
            var initialScale = buttonTrans.localScale;
            buttonTrans.localScale = initialScale / 1.2f;
            buttonTrans.LeanScale(initialScale, 0.25f).setEaseInOutBack().setOnComplete(callBack.Invoke);
        }

        public static void Move(Transform uiElement, Transform destination, LeanTweenType tweenType, CallBack callBack)
        {
            MoveWithDelay(uiElement , destination , tweenType , 0 , callBack);
        }
        public static void MoveWithDelay(Transform uiElement, Transform destination, LeanTweenType tweenType , float delay, CallBack callBack)
        {
            var button = uiElement.GetComponent<Button>();
            if(button != null) 
                button.interactable = false;
            
            uiElement.transform.LeanMoveLocal(destination.localPosition, 1f).setDelay(delay).setEase(tweenType).setOnComplete(
                () =>
                {
                    if(button != null) 
                        button.interactable = true;
                    callBack?.Invoke();
                });
        }

        public static void Scale(Transform uiElement, Vector3 initialScale , Vector3 targetScale, LeanTweenType tweenType, CallBack callBack)
        {
            ScaleWithDelay(uiElement , initialScale, targetScale , tweenType , 0 , callBack);
        }
        public static void ScaleWithDelay(Transform uiElement , Vector3 initialScale , Vector3 targetScale , LeanTweenType tweenType , float delay , CallBack callBack)
        {
            uiElement.transform.localScale = initialScale;
            uiElement.transform.LeanScale(targetScale, 1f).setEase(tweenType).setDelay(delay)
                .setOnComplete(() =>
                {
                    callBack?.Invoke();
                });
        }
    }
}