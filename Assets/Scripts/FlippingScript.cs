using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FlippingScript : MonoBehaviour
    {

        public bool flipped = false;
        public bool isFlipping = false;
        
        private float _rotationSpeed = 3f;

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