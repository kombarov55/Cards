using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class OnCardClick : MonoBehaviour, IPointerClickHandler
    {
        
        private OpenDescriptionScript _openDescriptionScript;
        private FlippingScript _flippingScript;

        public void Start()
        {
            _openDescriptionScript = GetComponent<OpenDescriptionScript>();
            _flippingScript = GetComponent<FlippingScript>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_flippingScript.flipped)
            {
                StartCoroutine(_flippingScript.Flip());
            }
            else
            {
                _openDescriptionScript.OpenDescription();
            }
        }
    }
}