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

                float time = 0.3f;
                
                StartCoroutine(MoveOverSeconds(cardObject, descPosition, time));
                StartCoroutine(RotateNTimes(cardObject, 2, time,  cardObject.transform.eulerAngles.z));
            }
        }
        
        private IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
            // speed should be 1 unit per second
            while (objectToMove.transform.position != end)
            {
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
                Debug.Log(objectToMove.transform.position);
                yield return new WaitForEndOfFrame();
            }
        }
        
        public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            while (elapsedTime < seconds)
            {
                objectToMove.transform.position = Vector3.Lerp(startingPos, end, elapsedTime / seconds);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
        }
        
        
        private IEnumerator RotateNTimes(GameObject objectToMove, int times, float seconds, float destDegree)
        {
            float degree = 360 * times + destDegree;
            float elapsedtime = 0;

            Vector3 startingRotation = transform.eulerAngles;
            Vector3 endingPos = new Vector3(startingRotation.x, startingRotation.y, degree);
             
            while (elapsedtime < seconds)
            {
//                objectToMove.transform.eulerAngles.Set(0f, 0f,  Time.deltaTime * seconds * degree);
                objectToMove.transform.eulerAngles = Vector3.Lerp(startingRotation, endingPos, elapsedtime / seconds);
                Debug.Log(objectToMove.transform.eulerAngles.z);
                elapsedtime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
            objectToMove.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}