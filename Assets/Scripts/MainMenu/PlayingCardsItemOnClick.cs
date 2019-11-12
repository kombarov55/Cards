using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayingCardsItemOnClick : MonoBehaviour, IPointerClickHandler
    {
        public GameObject targetAlignment;

        public void OnPointerClick(PointerEventData eventData)
        {
            StaticContext.alignmentName = targetAlignment.name;
            SceneManager.LoadScene("PlayingCards");
        }
    }
}