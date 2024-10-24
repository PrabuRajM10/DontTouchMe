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
            
            UiAnimator.Move(playButton.transform , playButtonEndPos , LeanTweenType.easeOutElastic);
            UiAnimator.Move(title.transform , titleTextStartPos , LeanTweenType.easeOutElastic);
            UiAnimator.Scale(title.transform,Vector3.zero,  Vector3.one, LeanTweenType.easeOutBack);
        }

        protected override void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlayButton);
        }

        private void OnClickPlayButton()
        {
            UiAnimator.ButtonOnClick(playButton , () =>
            {
                UiAnimator.Move(playButton.transform , playButtonStartPos , LeanTweenType.easeOutExpo,0.6f,0,false, () =>
                {
                    OnOnPlayButtonPressed?.Invoke();
                });
                UiAnimator.Scale(title.transform, title.transform.localScale, Vector3.zero, LeanTweenType.easeOutBack);
                UiAnimator.Move(title.transform, titleTextEndPos, LeanTweenType.easeOutElastic);
            });
        }
    }
}