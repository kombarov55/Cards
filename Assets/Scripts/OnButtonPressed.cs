using UnityEngine.UI;

namespace DefaultNamespace
{
    public class OnButtonPressed : UnityEngine.MonoBehaviour
    {

        public int value;
        
        public void OnClick()
        {
            var button = GetComponent<Button>();

            button.GetComponent<Text>().text = "value is " + value;
        }
    }
}