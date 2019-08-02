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
            
            cardService.RenderAlignment(cardService.alignments[0].name);
        }
    }
}