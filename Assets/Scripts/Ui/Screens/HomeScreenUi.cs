using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class HomeScreenUi : BaseUi
    {
        [SerializeField] private Button playButton;

        public event Action onPlayButtonPressed;
 
        private void OnEnable()
        {
            playButton.onClick.AddListener(OnClickPlayButton);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnClickPlayButton);
        }

        public override void Reset()
        {
            
        }

        private void OnClickPlayButton()
        {
            ButtonAnimator.Animate(playButton , () =>
            {
                onPlayButtonPressed?.Invoke();
            });
        }
    }
}