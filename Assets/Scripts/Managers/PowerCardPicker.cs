using System;
using System.Collections.Generic;
using Enums;
using Gameplay;
using UnityEngine;
using Unity.Collections;
using Enum = Enums.Enum;
using Random = UnityEngine.Random;

namespace Managers
{

    [Serializable]
    public class PowerCardProbability
    {
        public Enum.CardRarity cardRarity;
        
        [ Range(1,100)]
        public int probabilityInPercentage;

        public PowerCardProbability(Enum.CardRarity cardRarity , int probability)
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

        private Dictionary<Enum.CardRarity, int> _rarityMargin = new Dictionary<Enum.CardRarity, int>();

        private void OnValidate()
        {
            // var rarity = Enum.GetValues(typeof(CardRarity));
            //
            // if (powerCardProbabilities.Count == rarity.Length) return;
            // powerCardProbabilities.Clear();
            //
            // foreach (int r in rarity)
            // {
            //     powerCardProbabilities.Add(new PowerCardProbability((CardRarity)r, 10));
            // }
        }

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
            List<CardData> cardsList = new List<CardData>();
            var powerCardsPool = powerCardProbabilities;

            for (int i = 0; i < cardPickCount; i++)
            {
                // var card = powerCardsDataSo.GetRandomCard();
                // card.powerCard.Init();
                // cardsList.Add(card);
                var random = Random.Range(1, 100);
                Debug.LogFormat("[PowerCardManager] [GetCard] before random " + random);
                // foreach (var powerCardProbability in powerCardsPool)
                // {
                //     if (powerCardProbability.probabilityInPercentage >= random)
                //     {
                //         Debug.LogFormat("[PowerCardManager] [GetCard] powerCardProbability.probabilityInPercentage {1} " , random , powerCardProbability.probabilityInPercentage);
                //         var cardList = powerCardsDataSo.GetCardDataListByRarity(powerCardProbability.cardRarity);
                //
                //         var randomIndex = Random.Range(0, cardList.Count);
                //
                //         var theChosenOne = cardList[randomIndex];
                //
                //         Debug.Log("[PowerCardManager] [GetCard] theChosenOne " + (theChosenOne.cardId.ToString() , theChosenOne.cardRarity));
                //         // powerCardsPool.Remove(powerCardProbability);
                //         
                //         cardsList.Add(theChosenOne);
                //         break;
                //     }
                // }
                foreach (var powerCardProbability in _rarityMargin)
                {
                    if ( random <= powerCardProbability.Value)
                    {
                        Debug.LogFormat("[PowerCardManager] [GetCard] powerCardProbability.probabilityInPercentage {1} " , random , powerCardProbability.Value);
                        var cardList = powerCardsDataSo.GetCardDataListByRarity(powerCardProbability.Key);
                
                        var randomIndex = Random.Range(0, cardList.Count);
                
                        var theChosenOne = cardList[randomIndex];
                
                        Debug.Log("[PowerCardManager] [GetCard] theChosenOne " + (theChosenOne.cardId.ToString() , theChosenOne.cardRarity));
                        // powerCardsPool.Remove(powerCardProbability);
                        
                        cardsList.Add(theChosenOne);
                        break;
                    }
                }
            }
            return cardsList;
        }

        public List<CardData> GetPowerCards()
        {
            var currentGamePowerCards = GetCards();
            GameManager.Instance.SetPowerCardsForTheGame(currentGamePowerCards);
            return currentGamePowerCards;
        }
    }
}