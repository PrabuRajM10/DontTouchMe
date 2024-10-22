using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class HomeScreenScreen : BaseUi
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform playButtonStartPos;
        [SerializeField] private Transform playButtonEndPos;
        [SerializeField] private Transform titleTextStartPos;
        [SerializeField] private Transform titleTextEndPos;
        public event Action OnOnPlayButtonPressed;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnClickPlayButton);
            
            UiAnimator.MoveWithDelay(playButton.transform , playButtonEndPos , LeanTweenType.easeOutElastic , 0.3f, null);
            UiAnimator.MoveWithDelay(title.transform , titleTextStartPos , LeanTweenType.easeOutElastic , 0.3f, null);
            UiAnimator.ScaleWithDelay(title.transform,Vector3.zero,  Vector3.one, LeanTweenType.easeOutBack, 0.3f, null);
        }

        protected override void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlayButton);
        }

        private void OnClickPlayButton()
        {
            UiAnimator.ButtonOnClick(playButton , () =>
            {
                UiAnimator.Move(playButton.transform , playButtonStartPos , LeanTweenType.easeOutElastic , () =>
                {
                    OnOnPlayButtonPressed?.Invoke();
                });
                UiAnimator.Scale(title.transform, title.transform.localScale, Vector3.zero, LeanTweenType.easeOutBack, null);
                UiAnimator.Move(title.transform, titleTextEndPos, LeanTweenType.easeOutElastic, null);
            });
        }
    }
}