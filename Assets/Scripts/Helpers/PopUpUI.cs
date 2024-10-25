using System;
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
            });
        }

        private void OnClickConfirmButton()
        {
            LeanAnimator.ButtonOnClick(confirmButton , () =>
            {
                yesCallBack?.Invoke();
            });
        }
        private void OnClickNoButton()
        {
            LeanAnimator.ButtonOnClick(noButton , () =>
            {
                noCallBack?.Invoke();
            });
        }

        private void OnClickYesButton()
        {
            LeanAnimator.ButtonOnClick(yesButton , () =>
            {
                yesCallBack?.Invoke();
            });
        }

        public void ShowPop(string header , string message , PopUpType popUpType , YesCallBack yesCallBack , NoCallBack noCallBack )
        {
            HandleButtonsAndCallbacks(popUpType , yesCallBack , noCallBack);
            this.header.text = header;
            this.message.text = message;
            this.yesCallBack = yesCallBack;
            this.noCallBack = noCallBack;
            gameObject.SetActive(true);
        }

        public void ClosePopUp()
        {
            header.text = "";
            message.text = "";
            yesCallBack = null;
            noCallBack = null;
            gameObject.SetActive(false);
            DisableALlButtons();
        }

        void HandleButtonsAndCallbacks(PopUpType popUpType , YesCallBack yesCallBack , NoCallBack noCallBack)
        {
            DisableALlButtons();
            switch (popUpType)
            {
                case PopUpType.YES_NO:
                    this.yesCallBack = yesCallBack;
                    this.noCallBack = noCallBack;
                    yesButton.gameObject.SetActive(true);
                    noButton.gameObject.SetActive(true);
                    break;
                case PopUpType.ONLY_YES:
                    this.yesCallBack = yesCallBack;
                    yesButton.gameObject.SetActive(true);
                    break;
                case PopUpType.ONLY_NO:
                    this.noCallBack = noCallBack;
                    noButton.gameObject.SetActive(true);
                    break;
                case PopUpType.CONFIRM:
                    this.yesCallBack = yesCallBack;
                    confirmButton.gameObject.SetActive(true);
                    break;
                case PopUpType.CONTINUE:
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
