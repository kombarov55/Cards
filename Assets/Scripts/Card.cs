namespace DefaultNamespace
{
    public class Card
    {
        public CardSuit suit;
        public CardValue value;

        public Card() { }
        
        public Card(CardSuit suit, CardValue value)
        {
            this.suit = suit;
            this.value = value;
        }
    }
}