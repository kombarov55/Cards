using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ToPlayingCardsOverview : MonoBehaviour, IPointerClickHandler
    {
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene("PlayingCardsOverview");
        }
    }
}