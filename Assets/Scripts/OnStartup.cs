using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class OnStartup : MonoBehaviour
    {

        public CardService cardService;
    
        void Start()
        {
            cardService.LoadCards();
            
            cardService.currentAlignmentName = StaticContext.alignmentName;
        }
    }
}