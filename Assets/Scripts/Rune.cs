using UnityEngine;

namespace DefaultNamespace
{
    public class Rune
    {
        public Sprite sprite;
        public string title;
        public string description;
        
        public Rune() { }

        public Rune(Sprite sprite, string title, string description)
        {
            this.sprite = sprite;
            this.title = title;
            this.description = description;
        }
    }
}