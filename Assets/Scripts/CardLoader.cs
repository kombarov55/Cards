using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardLoader
    {
        public static List<Card> LoadCards(List<Sprite> sprites)
        {
            var cards = LoadCardsFromXml();
            var cardsWithSprites = BindSpritesToCardObjects(cards, sprites);

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
                var title = xmlNode.Attributes["Title"].Value;
                var description = xmlNode.Attributes["Description"].Value;

                
                Card card = new Card(title, description);
                
                cards.Add(card);
            }

            return cards;
   
        }

        private static List<Card> BindSpritesToCardObjects(List<Card> cards, List<Sprite> sprites)
        {
            foreach (var card in cards)
            {
                card.sprite = FindSpriteByName(sprites, card.title);
            }
            return cards;
        }

        private static Sprite FindSpriteByName(List<Sprite> sprites, string name)
        {
            foreach (var sprite in sprites)
            {
                if (sprite.name == name)
                {
                    return sprite;
                }
            }

            throw new Exception("Не найден спрайт с именем " + name);
        }
    }
}