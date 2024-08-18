using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using Unity.Collections;
using Random = UnityEngine.Random;

namespace Managers
{

    [Serializable]
    public class PowerCardProbability
    {
        public CardRarity cardRarity;
        
        [ Range(1,100)]
        public int probabilityInPercentage;

        public PowerCardProbability(CardRarity cardRarity , int probability)
        {
            this.cardRarity = cardRarity;
            probabilityInPercentage = probability;
        }
    }
    public class PowerCardPicker : GenericSingleton<PowerCardPicker>
    {
        [SerializeField]private List<PowerCardProbability> powerCardProbabilities = new List<PowerCardProbability>();
        [SerializeField]private List<CardData> currentGameCards = new List<CardData>();

        [SerializeField] private PowerCardsData powerCardsDataSo;
        [SerializeField] private int cardPickCount = 3;
        
        private void OnValidate()
        {
            var rarity = Enum.GetValues(typeof(CardRarity));

            if (powerCardProbabilities.Count == rarity.Length) return;
            powerCardProbabilities.Clear();

            foreach (int r in rarity)
            {
                powerCardProbabilities.Add(new PowerCardProbability((CardRarity)r, 10));
            }
        }

        public override void Awake()
        {
            base.Awake();
            powerCardsDataSo.Init();
        }

        private List<CardData> GetCards()
        {
            List<CardData> cardsList = new List<CardData>();
            var powerCardsPool = powerCardProbabilities;

            for (int i = 0; i < cardPickCount; i++)
            {
                var card = powerCardsDataSo.GetRandomCard();
                cardsList.Add(card);
                // var random = Random.Range(1, 100);
                // foreach (var powerCardProbability in powerCardsPool)
                // {
                //     if (powerCardProbability.probabilityInPercentage >= random)
                //     {
                //         Debug.LogFormat("[PowerCardManager] [GetCard] random {0} powerCardProbability.probabilityInPercentage {1} " , random , powerCardProbability.probabilityInPercentage);
                //         var cardList = powerCardsDataSo.GetCardDataListByRarity(powerCardProbability.cardRarity);
                //
                //         var randomIndex = Random.Range(0, cardList.Count);
                //
                //         var theChosenOne = cardList[randomIndex];
                //
                //         Debug.Log("[PowerCardManager] [GetCard] theChosenOne " + (theChosenOne.cardId.ToString() , theChosenOne.cardRarity));
                //         powerCardsPool.Remove(powerCardProbability);
                //         
                //         cardsList.Add(theChosenOne);
                //         break;
                //     }
                // }
            }
            return cardsList;
        }

        public List<CardData> GetPowerCards()
        {
            var powerCards = GetCards();
            currentGameCards = powerCards;

            GameManager.Instance.SetPowerCardsForTheGame(powerCards);
            
            return powerCards;
        }
    }
}