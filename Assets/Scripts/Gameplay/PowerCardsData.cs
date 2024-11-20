using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Enum = Enums.Enum;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create PowerCardData", fileName = "PowerCardData", order = 0)]
    public class PowerCardsData : ScriptableObject
    {
        [SerializeField] private List<CardData> cardDataList = new List<CardData>();

        private Dictionary<Enum.CardRarity, List<CardData>> cardDataByRarity = new Dictionary<Enum.CardRarity, List<CardData>>();

        public void Init()
        {
            ResetCardsState();
            
            SetCardsByRarity(Enum.CardRarity.Common);
            SetCardsByRarity(Enum.CardRarity.Rare);
            SetCardsByRarity(Enum.CardRarity.Epic);
            SetCardsByRarity(Enum.CardRarity.Legendary);
        }

        public void ResetCardsState()
        {
            foreach (var cardData in cardDataList)
            {
                cardData.powerCard.ResetState();
            }
        }

        public CardData GetCardDataById(Enum.PowerCardsId cardId)
        {
            return cardDataList.FirstOrDefault(cardData => cardData.cardId == cardId);
        }

        public List<CardData> GetCardDataListByRarity(Enum.CardRarity rarity)
        {
            return cardDataByRarity[rarity];
        }

        public CardData GetRandomCard()
        {
            return cardDataList[Random.Range(0, cardDataList.Count)];
            // return cardDataList[8];
        }

        void SetCardsByRarity(Enum.CardRarity rarity)
        {
            var cardList = cardDataList.Where(cardData => cardData.cardRarity == rarity).ToList();
            cardDataByRarity.Add(rarity , cardList);
        }
    }
    
    

    [Serializable]
    public class CardData
    {
        public Enum.PowerCardsId cardId;
        public Enum.CardRarity cardRarity;
        public Sprite cardImg;
        public string cardName;
        public string cardDescription;
        public PowerCard powerCard;
    }
}