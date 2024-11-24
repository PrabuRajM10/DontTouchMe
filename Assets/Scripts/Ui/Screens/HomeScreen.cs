using System;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class HomeScreen : BaseUi
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform playButtonStartPos;
        [SerializeField] private Transform playButtonEndPos;
        [SerializeField] private Transform quitButtonStartPos;
        [SerializeField] private Transform quitButtonEndPos;
        [SerializeField] private Transform titleTextStartPos;
        [SerializeField] private Transform titleTextEndPos;
        public event Action OnPlayButtonPressed;
        public event Action OnQuitButtonPressed;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnClickPlayButton);
            quitButton.onClick.AddListener(OnClickQuitButton);
            
            LeanAnimator.Move(playButton.transform , playButtonEndPos , LeanTweenType.easeOutElastic);
#if !UNITY_WEBGL
            LeanAnimator.Move(quitButton.transform , quitButtonEndPos , LeanTweenType.easeOutElastic , 1,0.1f);
#endif
            LeanAnimator.Move(title.transform , titleTextStartPos , LeanTweenType.easeOutElastic);
            LeanAnimator.Scale(title.transform,Vector3.zero,  Vector3.one, LeanTweenType.easeOutBack);
        }


        protected override void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlayButton);
            quitButton.onClick.RemoveListener(OnClickQuitButton);
        }

        private void OnClickQuitButton()
        {
            LeanAnimator.ButtonOnClick(quitButton , () =>
            {
                OnQuitButtonPressed?.Invoke();
            });
        }
        private void OnClickPlayButton()
        {
            LeanAnimator.ButtonOnClick(playButton , () =>
            {
                LeanAnimator.Move(quitButton.transform, quitButtonStartPos, LeanTweenType.easeOutExpo );
                LeanAnimator.Move(playButton.transform , playButtonStartPos , LeanTweenType.easeOutExpo,0.6f,0.1f,false, () =>
                {
                    OnPlayButtonPressed?.Invoke();
                });
                LeanAnimator.Scale(title.transform, title.transform.localScale, Vector3.zero, LeanTweenType.easeOutBack);
                LeanAnimator.Move(title.transform, titleTextEndPos, LeanTweenType.easeOutElastic);
            });
        }
    }
}