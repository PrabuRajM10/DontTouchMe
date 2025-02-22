using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Gameplay;
using UnityEngine;
using Unity.Collections;
using Random = UnityEngine.Random;

namespace Managers
{

    [Serializable]
    public class PowerCardProbability
    {
        public DTMEnum.CardRarity cardRarity;
        
        [ Range(1,100)]
        public int probabilityInPercentage;

        public PowerCardProbability(DTMEnum.CardRarity cardRarity , int probability)
        {
            this.cardRarity = cardRarity;
            probabilityInPercentage = probability;
        }
    }
    public class PowerCardPicker : GenericSingleton<PowerCardPicker>
    {
        [SerializeField]private List<PowerCardProbability> powerCardProbabilities = new List<PowerCardProbability>();

        [SerializeField] private PowerCardsData powerCardsDataSo;
        [SerializeField] private int cardPickCount = 3;

        private Dictionary<DTMEnum.CardRarity, int> _rarityMargin = new Dictionary<DTMEnum.CardRarity, int>();


        public override void Awake()
        {
            base.Awake();
            powerCardsDataSo.Init();
            int margin = 0;
            foreach (var powerCardProbability in powerCardProbabilities)
            {
                margin += powerCardProbability.probabilityInPercentage;
                _rarityMargin.Add(powerCardProbability.cardRarity , margin);
                Debug.LogFormat("[PowerCardManager] [Awake] _rarityMargin " + (powerCardProbability.cardRarity , margin));
            }
        }

        private List<CardData> GetCards()
        {
            powerCardsDataSo.ResetCardsState();
            var cardsList = new List<CardData>();
            for (int i = 0; i < cardPickCount; i++)
            {
                var random = Random.Range(1, 100);
                // Debug.LogFormat("[PowerCardManager] [GetCard] before random " + random);
                foreach (var theChosenOne in from powerCardProbability in _rarityMargin where random <= powerCardProbability.Value select GetUniqueCard(cardsList , powerCardProbability.Key))
                {
                    // Debug.Log("[PowerCardManager] [GetCard] theChosenOne " + (theChosenOne.cardId.ToString() , theChosenOne.cardRarity));
                        
                    cardsList.Add(theChosenOne);
                    break;
                }
            }
            return cardsList;
        }

        CardData GetUniqueCard(List<CardData> pickedCardsList , DTMEnum.CardRarity rarity)
        {
            var cardList = powerCardsDataSo.GetCardDataListByRarity(rarity);
            var randomIndex = Random.Range(0, cardList.Count);
            var theChosenOne = cardList[randomIndex];

            return !pickedCardsList.Contains(theChosenOne) ? theChosenOne : GetUniqueCard(pickedCardsList, rarity);
        }

        public List<CardData> GetPowerCards()
        {
            var currentGamePowerCards = GetCards();
            GameManager.Instance.SetPowerCardsForTheGame(currentGamePowerCards);
            return currentGamePowerCards;
        }
    }
}