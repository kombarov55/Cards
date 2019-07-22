using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FlippingScript : MonoBehaviour, IPointerClickHandler
    {

        public float rotationSpeed;

        public Sprite otherSprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            StartCoroutine(Flip());
        }

        /*
         * Перевернуть на 90 градусов за 0.5 сек
         * Есть скорость поворота - параметр
         * Делаю поворот по скорости и жду сл кадра, пока не повернётся на 90 градусов 
         */
        public IEnumerator Flip()
        {
            Debug.Log("Flip clicked");

            while (transform.rotation.eulerAngles.y < 90f)
            {
               transform.Rotate(new Vector3(0f, transform.rotation.y + rotationSpeed));
               yield return new WaitForEndOfFrame();
            }

            GetComponent<Image>().sprite = otherSprite;
            
            while (transform.rotation.eulerAngles.y < 180f)
            {
                transform.Rotate(new Vector3(0f, transform.rotation.y + rotationSpeed));
                yield return new WaitForEndOfFrame();
            }

        }
    }
}