using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ToRunesScript : MonoBehaviour
    {
        public void ToRunes()
        {
            SceneManager.LoadScene("Taro");
        }
    }
}