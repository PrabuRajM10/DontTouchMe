using System;
using System.Threading.Tasks;
using Helpers;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class GameResultScreen : BaseUi
    {
        [SerializeField] private TMP_Text wonTitle;
        [SerializeField] private TMP_Text loseTitle;
        [SerializeField] private TMP_Text coinsCount;
        [SerializeField] private TMP_Text killsCount;

        [SerializeField] private Button homeButton;
        [SerializeField] private Button retryButton;

        [SerializeField] private Transform title;
        [SerializeField] private Transform coins;
        [SerializeField] private Transform kills;
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
            
            LeanAnimator.Move(title.transform , titleEndPos , LeanTweenType.easeOutElastic);
            LeanAnimator.Move(retryButton.transform , retryBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.1f);
            LeanAnimator.Move(homeButton.transform , homeBtnEndPos , LeanTweenType.easeOutElastic , 1 , 0.2f);
            
            LeanAnimator.Scale(coins , Vector3.zero,  Vector3.one, LeanTweenType.easeOutBack);
            LeanAnimator.Scale(kills , Vector3.zero,  Vector3.one, LeanTweenType.easeOutBack);

        }

        protected override void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnClickHomeButton);
            retryButton.onClick.RemoveListener(OnClickRetryButton);
        }

        private void OnClickRetryButton()
        {
            LeanAnimator.ButtonOnClick(retryButton , () =>
            {
                OnClickUiAnimations(() =>
                {
                    OnRetryButtonPressed?.Invoke();
                });
            });
        }

        private void OnClickHomeButton()
        {
            LeanAnimator.ButtonOnClick(homeButton , () =>
            {
                OnClickUiAnimations(() =>
                {
                    OnHomeButtonPressed?.Invoke();
                });
            });
        }

        async void OnClickUiAnimations(Callback callback)
        {
            LeanAnimator.Move(title.transform , titleStartPos , LeanTweenType.easeOutElastic);
            LeanAnimator.Move(homeButton.transform , homeBtnStartPos , LeanTweenType.easeOutElastic , 1 , 0.1f);
            LeanAnimator.Move(retryButton.transform , retryBtnStartPos , LeanTweenType.easeOutElastic , 1 , 0.2f);

            LeanAnimator.Scale(coins , Vector3.one,  Vector3.zero, LeanTweenType.easeOutBack);
            LeanAnimator.Scale(kills , Vector3.one,  Vector3.zero, LeanTweenType.easeOutBack);
            
            await Task.Delay(400);
            callback?.Invoke();
        }

        public void ShowResult(bool successful, int currentCollectedCoins, int currentKillCount)
        {
            Debug.Log("[ShowResult] currentCollectedCoins , currentKillCount " + (currentCollectedCoins , currentKillCount));
            wonTitle.gameObject.SetActive(successful);
            loseTitle.gameObject.SetActive(!successful);
            retryButton.gameObject.SetActive(!successful);
            coinsCount.text = currentCollectedCoins.ToString();
            killsCount.text = currentKillCount.ToString();
        }
    }
}