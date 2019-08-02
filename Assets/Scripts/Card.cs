using UnityEngine;

namespace DefaultNamespace
{
    public class Card
    {
        public Sprite sprite;
        public string title;
        public string description;

        public Card() { }

        public Card(string title, string description)
        {
            this.title = title;
            this.description = description;
        }
    }
}