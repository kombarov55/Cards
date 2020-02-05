using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RuneSelectionScript : MonoBehaviour
    {
        public void OnBackClicked()
        {
            SceneManager.LoadScene("MainMenuRunes");
        }
    }
}