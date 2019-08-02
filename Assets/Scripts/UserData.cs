using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UserData : MonoBehaviour
    {
        public Card card;
        public CardService cardService;
        public Sprite shirt;
        
        public void Start()
        {
            GetComponent<Image>().sprite = shirt;
        }
    }
}