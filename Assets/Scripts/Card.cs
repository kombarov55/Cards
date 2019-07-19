using UnityEngine;

namespace DefaultNamespace
{
    public class Card
    {
        public CardSuit suit;
        public CardValue value;
        public Sprite sprite;

        public Card() { }
        
        public Card(CardSuit suit, CardValue value, Sprite sprite)
        {
            this.suit = suit;
            this.value = value;
            this.sprite = sprite;
        }
    }
}