using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FlippingScript : MonoBehaviour
    {

        public bool flipped = false;
        
        private float _rotationSpeed = 3f;

        public IEnumerator Flip()
        {
            Debug.Log("Flip clicked");

            while (transform.rotation.eulerAngles.y < 90f)
            {
               transform.Rotate(new Vector3(0f, transform.rotation.y + _rotationSpeed));
               Debug.Log("1: " + transform.rotation.eulerAngles.y);
               yield return new WaitForEndOfFrame();
            }

            GetComponent<Image>().sprite = GetComponent<UserData>().card.sprite;
            transform.Rotate(0f, 180f, 0f);
            Debug.Log("transform.Rotate(0f, 180f, 0f)=" + transform.rotation.eulerAngles.y);
            
            while (transform.rotation.eulerAngles.y < 350f)
            {
                transform.Rotate(new Vector3(0f, transform.rotation.y + _rotationSpeed));
                Debug.Log("2: " + transform.rotation.eulerAngles.y);
                yield return new WaitForEndOfFrame();
            }
            
            transform.rotation.eulerAngles.Set(0f, 0f, 0f);
            Debug.Log("flipped=true");
            flipped = true;
        }
    }
}