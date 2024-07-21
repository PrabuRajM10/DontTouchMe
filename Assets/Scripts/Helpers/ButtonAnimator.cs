using UnityEngine.UI;

namespace Helpers
{
    public abstract class ButtonAnimator
    {
        public delegate void CallBack();

        private static CallBack _callback;
        public static void Animate(Button button , CallBack callBack)
        {
            var buttonTrans = button.transform;
            _callback = callBack;
            var initialScale = buttonTrans.localScale;
            buttonTrans.localScale = initialScale / 1.2f;
            buttonTrans.LeanScale(initialScale, 0.25f).setEaseInOutBack().setOnComplete(OnComplete);
        }

        private static void OnComplete()
        {
            _callback.Invoke();
        }
    }
}