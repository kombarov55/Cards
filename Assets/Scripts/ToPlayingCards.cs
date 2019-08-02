using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ToPlayingCards : MonoBehaviour
    {
        public void TOPlayingcards()
        {
            SceneManager.LoadScene("PlayingCards");
        }
    }
}