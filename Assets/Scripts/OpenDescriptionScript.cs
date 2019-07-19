using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{

    public class OpenDescriptionScript : MonoBehaviour, IPointerClickHandler
    {
        
        public CardService CardService;

        public void OnPointerClick(PointerEventData eventData)
        {
            var card = GetComponent<UserData>().card;
            CardService.OpenDescription(card);
        }
    }
}