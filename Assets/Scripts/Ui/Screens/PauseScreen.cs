using System;
using System.Threading.Tasks;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class PauseScreen : BaseUi
    {
        [SerializeField] private Button resumeButton;
        [FormerlySerializedAs("mainMenuButton")] [SerializeField] private Button homeButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private Transform resumeBtnStartPos;
        [SerializeField] private Transform homeBtnStartPos;
        [SerializeField] private Transform quitBtnStartPos;
        
        [SerializeField] private Transform resumeBtnEndPos;
        [SerializeField] private Transform homeBtnEndPos;
        [SerializeField] private Transform quitBtnEndPos;

        public event Action OnResumeButtonPressed;
        public event Action OnHomeButtonPressed;
        public event Action OnQuitButtonPressed;

        private delegate void Callback();
        

        private void OnEnable()
        {
            Debug.Log("[PauseScreen] [OnEnable] called ");
            resumeButton.onClick.AddListener(OnClickResumeButton);
            homeButton.onClick.AddListener(OnClickHomeButton);
            quitButton.onClick.AddListener(OnClickQuitButton);
            LeanAnimator.Move(resumeButton.transform , resumeBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.1f ,true);
            LeanAnimator.Move(homeButton.transform , homeBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.2f ,true);
            LeanAnimator.Move(quitButton.transform , quitBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.3f ,true);

        }

        protected override void OnDisable()
        {
            base.OnDisable();
            resumeButton.onClick.RemoveListener(OnClickResumeButton);
            homeButton.onClick.RemoveListener(OnClickHomeButton);
            quitButton.onClick.RemoveListener(OnClickQuitButton);
        }

        private void OnClickQuitButton()
        {
            LeanAnimator.ButtonOnClick(quitButton, () =>
            {
                OnClickAnimation(() =>
                {
                    OnQuitButtonPressed?.Invoke();
                });
            }, true);
        }

        private void OnClickHomeButton()
        {
            LeanAnimator.ButtonOnClick(homeButton, () =>
            {
                OnClickAnimation(() =>
                {
                    OnHomeButtonPressed?.Invoke();
                });
            }, true);
        }

        private void OnClickResumeButton()
        {
            LeanAnimator.ButtonOnClick(resumeButton, () =>
            {
                OnClickAnimation(() =>
                {
                    OnResumeButtonPressed?.Invoke();
                });
            } , true);
        }

        async void OnClickAnimation(Callback callback)
        {
            LeanAnimator.Move(resumeButton.transform , resumeBtnStartPos , LeanTweenType.easeOutElastic, 1 , 0.1f ,true);
            LeanAnimator.Move(homeButton.transform , homeBtnStartPos , LeanTweenType.easeOutElastic, 1 , 0.2f ,true);
            LeanAnimator.Move(quitButton.transform, quitBtnStartPos, LeanTweenType.easeOutElastic, 1, 0.3f, true);

            await Task.Delay(600);
            callback?.Invoke();
        }
    }
}