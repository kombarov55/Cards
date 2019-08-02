using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TransformHelper : MonoBehaviour
    {
        public void MoveAllToDeck(List<GameObject> cardObjects, Vector3 deck)
        {
            Vector3 lastCardInDeckPosition = deck;
            
            foreach (var cardObject in cardObjects)
            {
                cardObject.transform.position = lastCardInDeckPosition + new Vector3(-5f, 5f);
                lastCardInDeckPosition = cardObject.transform.position;
            }
        }

        public void MoveAllToDestPositions(List<GameObject> cardObjects)
        {
            foreach (var cardObject in cardObjects)
            {
                var userData = cardObject.GetComponent<UserData>();
                var descPosition = userData.destPosition;
                StartCoroutine(MoveOverSpeed(cardObject, descPosition, 1000f));
            }
        }
        
        private IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
            // speed should be 1 unit per second
            Debug.Log("Move called ");
            while (objectToMove.transform.position != end)
            {
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
                Debug.Log(objectToMove.transform.position);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}