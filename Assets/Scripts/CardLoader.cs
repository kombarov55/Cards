using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardLoader
    {
        public static List<Card> LoadCards(List<Sprite> hearts, List<Sprite> clubs, List<Sprite> spades, List<Sprite> diamonds)
        {
            var cards = LoadCardsFromXml();
            var cardsWithSprites = BindSpritesToCardObjects(cards, hearts, clubs, spades, diamonds);

            return cardsWithSprites;
        }

        private static List<Card> LoadCardsFromXml()
        {
            List<Card> cards = new List<Card>();
            
            var textAsset = Resources.Load<TextAsset>("PlayingCards");
            var xml = textAsset.text;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var xmlNodes = xmlDocument.SelectNodes("/Cards/Card");
            
            foreach (XmlNode xmlNode in xmlNodes)
            {
                var suit = xmlNode.Attributes["Suit"].Value;
                var value = xmlNode.Attributes["Value"].Value;
                var title = xmlNode.Attributes["Title"].Value;
                var description = xmlNode.Attributes["Description"].Value;

                var enumSuit = (CardSuit) Enum.Parse(typeof(CardSuit), suit.ToUpper());
                var enumValue = (CardValue) Enum.Parse(typeof(CardValue), value.ToUpper());
                
                Card card = new Card(enumSuit, enumValue, title, description);
                
                cards.Add(card);
            }

            return cards;
   
        }

        private static List<Card> BindSpritesToCardObjects(List<Card> cards, List<Sprite> hearts, List<Sprite> clubs, List<Sprite> spades, List<Sprite> diamonds)
        {
            /*
             * Все спрайты лежат в порядке от 6 до туза. Элементы енума CardValue расположены в таком же порядке.   
             * Для того чтобы получить нужный спрайт, нужно взять ordinal enum-а и использовать значение как
             * индекс в массиве нужной масти. 
             */

            foreach (var card in cards)
            {
                var suit = card.suit;
                var value = card.value;
                
                List<Sprite> currentSpriteList;

                switch (suit) 
                {
                    case CardSuit.HEART:
                        currentSpriteList = hearts;
                        break;
                    case CardSuit.CLUB:
                        currentSpriteList = clubs;
                        break;
                    case CardSuit.SPADE:
                        currentSpriteList = spades;
                        break;
                    case CardSuit.DIAMOND:
                        currentSpriteList = diamonds;
                        break;
                    default: throw new Exception("ИДИ ВПИЗДУ ЕБУЧИЙ СИШАРП БЛЯТЬ. ЕНУМОВ ЕМУ НЕ ХВАТАЕТ");
                }

                int ordinal = (int) value;
                var sprite = currentSpriteList[ordinal];

                card.sprite = sprite;
            }

            return cards;
        }
    }
}