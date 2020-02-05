using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardLoader
    {
        public static List<Card> LoadCards(string path, List<Sprite> sprites)
        {
            var cards = LoadCardsFromXml(path);
            var cardsWithSprites = BindSpritesToCardObjects(cards, sprites);

            return cardsWithSprites;
        }

        private static List<Card> LoadCardsFromXml(string path)
        {
            List<Card> cards = new List<Card>();
            
            var textAsset = Resources.Load<TextAsset>(path);
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
            List<Card> rs = new List<Card>();
            
            foreach (var sprite in sprites)
            {
                var card = FindCardByName(cards, sprite.name);
                card.sprite = sprite;
                rs.Add(card);
            }
        
//            foreach (var card in cards)
//            {
//                card.sprite = FindSpriteByName(sprites, card.title);
//            }
            return rs;
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
        
        private static Card FindCardByName(List<Card> cards, string name)
        {
            foreach (var card in cards)
            {
                if (card.title == name)
                {
                    return card;
                }
            }

            throw new Exception("Не найден спрайт с именем " + name);
        }
    }
}