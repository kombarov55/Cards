using UnityEngine;

namespace DefaultNamespace
{
    public class CloseButtonScript : MonoBehaviour
    {
        public void OnClick()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}