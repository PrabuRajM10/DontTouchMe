using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class PopUpUI : MonoBehaviour
    {

        [SerializeField] private Button yesButton; 
        [SerializeField] private Button noButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button continueButton;

        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI message;

        [SerializeField] private Transform board;
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;

        public delegate void YesCallBack();

        public delegate void NoCallBack();

        private YesCallBack yesCallBack;
        private NoCallBack noCallBack;

        private void OnEnable()
        {
            yesButton.onClick.AddListener(OnClickYesButton);
            noButton.onClick.AddListener(OnClickNoButton);
            confirmButton.onClick.AddListener(OnClickConfirmButton);
            continueButton.onClick.AddListener(OnCLickContinueButton);
            yesButton.transform.localScale = Vector3.one;
            noButton.transform.localScale = Vector3.one;
            
        }


        private void OnDisable()
        {
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            confirmButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
        }

        private void OnCLickContinueButton()
        {
            LeanAnimator.ButtonOnClick(continueButton , () =>
            {
                yesCallBack?.Invoke();
            }, true);
        }

        private void OnClickConfirmButton()
        {
            LeanAnimator.ButtonOnClick(confirmButton , () =>
            {
                yesCallBack?.Invoke();
            }, true);
        }
        private void OnClickNoButton()
        {
            LeanAnimator.ButtonOnClick(noButton , () =>
            {
                noCallBack?.Invoke();
            }, true);
        }

        private void OnClickYesButton()
        {
            LeanAnimator.ButtonOnClick(yesButton , () =>
            {
                yesCallBack?.Invoke();
            } , true);
        }

        public void ShowPop(string header , string message , DTMEnum.PopUpType popUpType , YesCallBack yesCallBack , NoCallBack noCallBack )
        {
            HandleButtonsAndCallbacks(popUpType , yesCallBack , noCallBack);
            this.header.text = header;
            this.message.text = message;
            this.yesCallBack = yesCallBack;
            this.noCallBack = noCallBack;
            gameObject.SetActive(true);
            LeanAnimator.Fade(transform, 1, LeanTweenType.easeInOutSine, 0.5f , 0 , true);
            LeanAnimator.Move(board , endPos , LeanTweenType.easeOutElastic ,1f , 0 , true);
        }

        public void ClosePopUp()
        {
            LeanAnimator.Move(board , startPos , LeanTweenType.easeOutBack, 0.6f,0,true , () =>
            {
                header.text = "";
                message.text = "";
                yesCallBack = null;
                noCallBack = null;
                gameObject.SetActive(false);
                DisableALlButtons();
            });
            LeanAnimator.Fade(transform, 0, LeanTweenType.easeInOutSine, 0.5f , 0 , true);
        }

        void HandleButtonsAndCallbacks(DTMEnum.PopUpType popUpType , YesCallBack yesCallBack , NoCallBack noCallBack)
        {
            DisableALlButtons();
            switch (popUpType)
            {
                case DTMEnum.PopUpType.YES_NO:
                    this.yesCallBack = yesCallBack;
                    this.noCallBack = noCallBack;
                    yesButton.gameObject.SetActive(true);
                    noButton.gameObject.SetActive(true);
                    break;
                case DTMEnum.PopUpType.ONLY_YES:
                    this.yesCallBack = yesCallBack;
                    yesButton.gameObject.SetActive(true);
                    break;
                case DTMEnum.PopUpType.ONLY_NO:
                    this.noCallBack = noCallBack;
                    noButton.gameObject.SetActive(true);
                    break;
                case DTMEnum.PopUpType.CONFIRM:
                    this.yesCallBack = yesCallBack;
                    confirmButton.gameObject.SetActive(true);
                    break;
                case DTMEnum.PopUpType.CONTINUE:
                    this.yesCallBack = yesCallBack;
                    continueButton.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(popUpType), popUpType, null);
            }
        }
        

        void DisableALlButtons()
        {
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }
    }
}
