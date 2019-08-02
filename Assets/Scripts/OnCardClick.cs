using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class OnCardClick : MonoBehaviour, IPointerClickHandler
    {
        
        private FlippingScript _flippingScript;
        private UserData _userData;

        public void Start()
        {
            _flippingScript = GetComponent<FlippingScript>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_flippingScript.isFlipping) return;
            
            if (!_flippingScript.flipped)
            {
                StartCoroutine(_flippingScript.Flip());
            }
            else
            {
                _userData.cardService.OpenDescription(_userData.card);
            }
        }
    }
}