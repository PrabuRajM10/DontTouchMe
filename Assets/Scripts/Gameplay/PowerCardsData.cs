using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create PowerCardData", fileName = "PowerCardData", order = 0)]
    public class PowerCardsData : ScriptableObject
    {
        [SerializeField] private List<CardData> cardDataList = new List<CardData>();

        private Dictionary<DTMEnum.CardRarity, List<CardData>> cardDataByRarity = new Dictionary<DTMEnum.CardRarity, List<CardData>>();

        public void Init()
        {
            ResetCardsState();
            
            SetCardsByRarity(DTMEnum.CardRarity.Common);
            SetCardsByRarity(DTMEnum.CardRarity.Rare);
            SetCardsByRarity(DTMEnum.CardRarity.Epic);
            SetCardsByRarity(DTMEnum.CardRarity.Legendary);
        }

        public void ResetCardsState()
        {
            foreach (var cardData in cardDataList)
            {
                cardData.powerCard.ResetState();
            }
        }

        public CardData GetCardDataById(DTMEnum.PowerCardsId cardId)
        {
            return cardDataList.FirstOrDefault(cardData => cardData.cardId == cardId);
        }

        public List<CardData> GetCardDataListByRarity(DTMEnum.CardRarity rarity)
        {
            return cardDataByRarity[rarity];
        }

        public CardData GetRandomCard()
        {
            return cardDataList[Random.Range(0, cardDataList.Count)];
            // return cardDataList[8];
        }

        void SetCardsByRarity(DTMEnum.CardRarity rarity)
        {
            var cardList = cardDataList.Where(cardData => cardData.cardRarity == rarity).ToList();
            cardDataByRarity.Add(rarity , cardList);
        }
    }
    
    

    [Serializable]
    public class CardData
    {
        public DTMEnum.PowerCardsId cardId;
        public DTMEnum.CardRarity cardRarity;
        public Sprite cardImg;
        public string cardName;
        public string cardDescription;
        public PowerCard powerCard;
    }
}