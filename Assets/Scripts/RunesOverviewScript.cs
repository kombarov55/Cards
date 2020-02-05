using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RunesOverviewScript : MonoBehaviour
    {
        public string pathToXml = "Runes";
        public string prevSceneName = "RunesAlignmentSelection";
        public List<Sprite> sprites;
        public GameObject playingCardsOverviewAlignment;
        public GameObject rootPanel;
        public GameObject deck;
        public GameObject descriptionPopupPrefab;
        

        private TransformHelper _transformHelper;
        private GameObject instantiatedDescriptionPopup;
        
        /**
         * При старте:
         * 1. Выложить из колоды на позиции все карты
         * 2. Перевернуть по очереди
         */
        public void Start()
        {
            _transformHelper = GetComponent<TransformHelper>(); 
            

            List<Card> cards = CardLoader.LoadCards(pathToXml, sprites);
            var instantiatedAlignment = Instantiate(playingCardsOverviewAlignment, rootPanel.transform);


            var images = instantiatedAlignment.GetComponentsInChildren<Image>();

            List<GameObject> cardGameObjects = new List<GameObject>();

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var image = images[i];

                image.sprite = card.sprite;

                var userData = image.GetComponent<UserData>();
                userData.card = card;
                userData.destPosition = image.transform.position;
                userData.destDegree = image.transform.eulerAngles.z;
                
                cardGameObjects.Add(image.gameObject);
                
                image.gameObject.AddComponent<ActionBehaviour>();
                image.gameObject.GetComponent<ActionBehaviour>().action = () => OpenDescription(card);
            }
            
            _transformHelper.MoveAllToDeck(cardGameObjects, deck.transform.position);
            _transformHelper.MoveAllToDestPositions(cardGameObjects);
        }
        
        public void OpenDescription(Card card)
        {
            instantiatedDescriptionPopup = Instantiate(descriptionPopupPrefab, rootPanel.transform);
            var content = instantiatedDescriptionPopup.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0);

            Image img = null;
            Text title = null;
            Text description = null;
        
            for (var i = 0; i < content.transform.childCount; i++)
            {
                var child = content.transform.GetChild(i);
                if (child.name == "Image") img = child.gameObject.GetComponent<Image>();
                if (child.name == "Title") title = child.gameObject.GetComponent<Text>();
                if (child.name == "Description") description = child.gameObject.GetComponent<Text>();
            }
        
            var closeButton = instantiatedDescriptionPopup.transform.GetChild(1).gameObject.GetComponent<Button>();

            img.sprite = card.sprite;
            title.text = card.title;
            description.text = card.description;
        
            closeButton.gameObject.AddComponent<ActionBehaviour>();
            closeButton.gameObject.GetComponent<ActionBehaviour>().action = () => Destroy(instantiatedDescriptionPopup);

        }
        
        public void OnBackClicked()
        {
            SceneManager.LoadScene(prevSceneName);
        }
    }
}