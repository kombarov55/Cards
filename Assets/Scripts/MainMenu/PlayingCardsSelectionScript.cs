using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayingCardsSelectionScript : MonoBehaviour
    {
        public void OnBackClicked()
        {
            SceneManager.LoadScene("MainMenuPlayingCards");
        }
    }
}