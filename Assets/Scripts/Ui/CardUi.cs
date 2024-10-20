using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class CardUi : MonoBehaviour
    {
        [SerializeField] private Image cardImage;
        
        [SerializeField] private ImageFillLoader activeTimeFill;
        [SerializeField] private ImageFillLoader cooldownTimeFill;
        
        [SerializeField] private TMP_Text cardName;
        [SerializeField] private TMP_Text cardCost;

        [SerializeField] private GameObject unAvailableTint;

        

        private void OnEnable()
        {
            GameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameEnd(bool obj)
        {
            activeTimeFill.Reset();
            cooldownTimeFill.Reset();
            SetAvailability(false);
        }

        public void SetData(string cardName , Sprite cardImage , int xpCost)
        {
            this.cardName.text = cardName;
            cardCost.text = xpCost.ToString();
            this.cardImage.sprite = cardImage;
            SetAvailability(false);
        }

        public void SetAvailability(bool isAvailable)
        {
            unAvailableTint.SetActive(!isAvailable);
        }

        public void SetActiveTimer(float time)
        {
            activeTimeFill.StartLoading(time);
        }
        
        public void SetCooldownTimer(float time)
        {
            cooldownTimeFill.StartLoading(time);
        }
    }
}
