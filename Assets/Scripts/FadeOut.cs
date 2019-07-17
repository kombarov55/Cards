using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    
    public class FadeOut : MonoBehaviour
    {

        public float duration;

        private Image imageToFadeOut;
        
        
        public void Awake()
        {
            imageToFadeOut = GetComponent<Image>();
            StartCoroutine(FadeOutCoroutine());
        }

        public IEnumerator FadeOutCoroutine()
        {
        
            float frameDuration = 1f / 30f;
            float times = duration / frameDuration;
            float fadePercentPerTime = 1f / times;
            float waitTime = duration / times;

            Debug.Log("fading " + times + " times by " + fadePercentPerTime + ". WaitTime=" + waitTime + ", fade time=" + duration);

            for (float i = 0; i < times; i++)
            {
                var color = imageToFadeOut.color;
                color.a = color.a - fadePercentPerTime;
                imageToFadeOut.color = color;
                
                yield return null;
            }
        }

    }
}