using System;
using System.Threading.Tasks;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameResultScreen : BaseUi
    {
        [SerializeField] private TMP_Text wonTitle;
        [SerializeField] private TMP_Text loseTitle;

        [SerializeField] private Button homeButton;
        [SerializeField] private Button retryButton;

        [SerializeField] private Transform title;
        [SerializeField] private Transform titleStartPos;
        [SerializeField] private Transform titleEndPos;
        [SerializeField] private Transform homeBtnStartPos;
        [SerializeField] private Transform homeBtnEndPos;
        [SerializeField] private Transform retryBtnStartPos;
        [SerializeField] private Transform retryBtnEndPos;

        private delegate void Callback();

        public event Action OnRetryButtonPressed;
        public event Action OnHomeButtonPressed;
        
        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnClickHomeButton);
            retryButton.onClick.AddListener(OnClickRetryButton);
            
            UiAnimator.Move(title.transform , titleEndPos , LeanTweenType.easeOutElastic);
            UiAnimator.Move(retryButton.transform , retryBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.1f);
            UiAnimator.Move(homeButton.transform , homeBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.2f);

        }

        protected override void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnClickHomeButton);
            retryButton.onClick.RemoveListener(OnClickRetryButton);
        }

        private void OnClickRetryButton()
        {
            UiAnimator.ButtonOnClick(retryButton , () =>
            {
                OnClickUiAnimations(() =>
                {
                    OnRetryButtonPressed?.Invoke();
                });
            });
        }

        private void OnClickHomeButton()
        {
            UiAnimator.ButtonOnClick(homeButton , () =>
            {
                OnClickUiAnimations(() =>
                {
                    OnHomeButtonPressed?.Invoke();
                });
            });
        }

        public void ShowResult(bool successful)
        {
            wonTitle.gameObject.SetActive(successful);
            loseTitle.gameObject.SetActive(!successful);
            retryButton.gameObject.SetActive(!successful);
        }

        async void OnClickUiAnimations(Callback callback)
        {
            UiAnimator.Move(title.transform , titleStartPos , LeanTweenType.easeOutElastic);
            UiAnimator.Move(homeButton.transform , homeBtnStartPos , LeanTweenType.easeOutElastic , 1 , 0.1f);
            UiAnimator.Move(retryButton.transform , retryBtnStartPos , LeanTweenType.easeOutElastic , 1 , 0.2f);

            await Task.Delay(400);
            callback?.Invoke();
        }
    }
}