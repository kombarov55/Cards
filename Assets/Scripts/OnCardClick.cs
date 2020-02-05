using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class OnCardClick : MonoBehaviour, IPointerClickHandler
    {
        public bool shouldFlip = true;
        
        private FlippingScript _flippingScript;
        private UserData _userData;
        
        private bool flipped = false;
        private bool isFlipping = false;
        
        private float _rotationSpeed = 3f;

        public void Start()
        {
            _userData = GetComponent<UserData>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (shouldFlip)
            {
                if (isFlipping) return;

                if (!flipped)
                {
                    StartCoroutine(Flip());
                }
                else
                {
                    _userData.cardService.OpenDescription(_userData.card);
                }
            }
            else
            {
                _userData.cardService.OpenDescription(_userData.card);
            }
        }
        
        public IEnumerator Flip()
        {
            isFlipping = true;
            while (transform.rotation.eulerAngles.y < 90f)
            {
                transform.Rotate(new Vector3(0f, transform.rotation.y + _rotationSpeed));
                yield return new WaitForEndOfFrame();
            }

            GetComponent<Image>().sprite = GetComponent<UserData>().card.sprite;
            transform.Rotate(0f, 180f, 0f);
            
            while (transform.rotation.eulerAngles.y < 350f)
            {
                transform.Rotate(new Vector3(0f, transform.rotation.y + _rotationSpeed));
                yield return new WaitForEndOfFrame();
            }
            
            transform.rotation.eulerAngles.Set(0f, 0f, 0f);
            flipped = true;
            isFlipping = false;
        }
    }
}