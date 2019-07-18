using UnityEngine;

namespace DefaultNamespace
{
    public class OnStartup : MonoBehaviour
    {

        public CardService cardService;
    
        void Start()
        {
            cardService.Refresh();
        }
    }
}