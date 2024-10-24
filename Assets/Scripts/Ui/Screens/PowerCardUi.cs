using Gameplay;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class PowerCardUi : MonoBehaviour
    {
        [SerializeReference] private Image cardImg;
        [SerializeReference] private TMP_Text cardName;

        public void SetData(CardData powerCard, Transform cardsEndPosition)
        {
            cardImg.sprite = powerCard.cardImg;
            cardName.text = powerCard.cardName;
            
            gameObject.SetActive(true);
            UiAnimator.Move(transform , cardsEndPosition , LeanTweenType.easeOutBounce);
        }
    }
}