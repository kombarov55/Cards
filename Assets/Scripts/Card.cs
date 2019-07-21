using UnityEngine;

namespace DefaultNamespace
{
    public class Card
    {
        public CardSuit suit;
        public CardValue value;
        public Sprite sprite;
        public string title;
        public string description;

        public Card() { }

        public Card(CardSuit suit, CardValue value, string title, string description)
        {
            this.suit = suit;
            this.value = value;
            this.title = title;
            this.description = description;
        }
    }
}