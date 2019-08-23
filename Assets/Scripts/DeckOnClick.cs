using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class DeckOnClick : MonoBehaviour, IPointerClickHandler
    {

        public GameObject scripts;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var cardService = scripts.GetComponent<CardService>();

            if (cardService.IsAlignmentRendered() == false)
            {
                cardService.RenderAlignment(cardService.currentAlignmentName);
            }
        }
    }
}