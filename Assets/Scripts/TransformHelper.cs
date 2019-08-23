using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TransformHelper : MonoBehaviour
    {

        private GameObject _trashDeck;

        public void Start()
        {
            _trashDeck = GetComponent<CardService>().trashDeck;
        }
        
        public void MoveAllToDeck(List<GameObject> cardObjects, Vector3 deck)
        {
            foreach (var cardObject in cardObjects)
            {
                cardObject.transform.position = deck;
            }
        }

        public void MoveAllToDestPositions(List<GameObject> cardObjects)
        {
            StartCoroutine(MoveAllToDestPositionsCoroutine(cardObjects));
        }

        private IEnumerator MoveAllToDestPositionsCoroutine(List<GameObject> cardObjects)
        {
            foreach (var cardObject in cardObjects)
            {
                var userData = cardObject.GetComponent<UserData>();
                var descPosition = userData.destPosition;

                float time = 0.3f;
                
                StartCoroutine(MoveOverSeconds(cardObject, descPosition, time));
                StartCoroutine(RotateNTimes(cardObject, 1, time,  cardObject.transform.eulerAngles.z));
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        public IEnumerator ClearCoroutine(List<GameObject> cardObjects, List<Text> textElements)
        {
            float time = 0.5f;
            
            foreach (var cardObject in cardObjects)
            {
                StartCoroutine(MoveOverSeconds(cardObject, _trashDeck.transform.position, time));
                StartCoroutine(RotateNTimes(cardObject, 5, time,  cardObject.transform.eulerAngles.z));
            }

            foreach (var text in textElements)
            {
                StartCoroutine(FadeOutCoroutine(text, time));
            }
            
            yield return new WaitForSeconds(time);
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

        private IEnumerator FadeOutCoroutine(Text text, float fadeOutTime)
        { 
            Color originalColor = text.color;
            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/fadeOutTime));
                yield return null;
            }
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