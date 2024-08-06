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
    public class PowerCardManager : GenericSingleton<PowerCardManager>
    {
        [SerializeField]private List<PowerCardProbability> powerCardProbabilities = new List<PowerCardProbability>();

        [SerializeField] private PowerCardsData powerCardsDataSo;


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

        public void GetCard()
        {
            var random = Random.Range(1, 100);


            foreach (var powerCardProbability in powerCardProbabilities)
            {
                if (powerCardProbability.probabilityInPercentage >= random)
                {
                    Debug.LogFormat("[PowerCardManager] [GetCard] random {0} powerCardProbability.probabilityInPercentage {1} " , random , powerCardProbability.probabilityInPercentage);
                    var cardList = powerCardsDataSo.GetCardDataListByRarity(powerCardProbability.cardRarity);

                    var randomIndex = Random.Range(0, cardList.Count);

                    var theChosenOne = cardList[randomIndex];

                    Debug.Log("[PowerCardManager] [GetCard] theChosenOne " + (theChosenOne.cardId.ToString() , theChosenOne.cardRarity));
                    
                    return;
                }
            }
        }
    }
}