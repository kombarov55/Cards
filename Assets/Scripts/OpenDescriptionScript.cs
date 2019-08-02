using UnityEngine;

namespace DefaultNamespace
{

    public class OpenDescriptionScript : MonoBehaviour
    {

        private UserData _userData;

        public void Start()
        {
            _userData = GetComponent<UserData>();
        }

        public void OpenDescription()
        {
            var card = GetComponent<UserData>().card;
            _userData.cardService.OpenDescription(card);
        }
    }
}