using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create PowerCardData", fileName = "PowerCardData", order = 0)]
    public class PowerCardsData : ScriptableObject
    {
        [SerializeField] private List<CardData> cardDataList = new List<CardData>();

        private Dictionary<CardRarity, List<CardData>> cardDataByRarity = new Dictionary<CardRarity, List<CardData>>();

        public void Init()
        {
            SetCardsByRarity(CardRarity.Common);
            SetCardsByRarity(CardRarity.Rare);
            SetCardsByRarity(CardRarity.Epic);
            SetCardsByRarity(CardRarity.Legendary);
        }

        public CardData GetCardDataById(PowerCardsId cardId)
        {
            return cardDataList.FirstOrDefault(cardData => cardData.cardId == cardId);
        }

        public List<CardData> GetCardDataListByRarity(CardRarity rarity)
        {
            return cardDataByRarity[rarity];
        }

        public CardData GetRandomCard()
        {
            return cardDataList[Random.Range(0, cardDataList.Count)];
        }

        void SetCardsByRarity(CardRarity rarity)
        {
            var cardList = cardDataList.Where(cardData => cardData.cardRarity == rarity).ToList();
            cardDataByRarity.Add(rarity , cardList);
        }
    }
    
    public enum PowerCardsId
    {
        PC1 = 1,
        PC2,
        PC3,
        PC4,
        PC5,
        PC6,
        PC7,
        PC8,
        PC9,
        PC10
    }

    public enum CardRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public class CardData
    {
        public PowerCardsId cardId;
        public CardRarity cardRarity;
        public Sprite cardImg;
        public string cardName;
        public string cardDescription;
    }
}