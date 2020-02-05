using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenuScripts : MonoBehaviour
    {
    
        public void ToPlayingCardsAlignmentSelection()
        {
            SceneManager.LoadScene("PlayingCardsAlignmentSelection");
        }

        public void ToRunesAlignmentSelection()
        {
            SceneManager.LoadScene("RunesAlignmentSelection");
        }


        public void ToPlayingCardsOverview()
        {
            SceneManager.LoadScene("PlayingCardsOverview");
        }

        public void ToRunesOverview()
        {
            SceneManager.LoadScene("RunesOverview");
        }
    }
}