using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardService : MonoBehaviour
{

    public string pathToXml;
    
    public List<Sprite> sprites;
    public Sprite shirt;
    
    public GameObject deck;

    public List<GameObject> alignmentPrefabs;
    public GameObject rootPanel;
    public GameObject descriptionPopupPrefab;

    public string currentAlignmentName;
    
    private GameObject currentAlignment;
    private GameObject descriptionPopup;
    
    private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();
    
    List<GameObject> currentCardGameObjects = new List<GameObject>();

    private TransformHelper _transformHelper;

    public void Start()
    {
        _transformHelper = GetComponent<TransformHelper>();
    }

    public void LoadCards()
    {
        cards = CardLoader.LoadCards(pathToXml, sprites);        
    }

    public void Clear()
    {
        if (currentAlignment != null)
        {
            Destroy(currentAlignment);
        }

        usedCards.Clear();
        currentCardGameObjects.Clear();
    }

    public void MoveAllToDestPositions()
    {
        _transformHelper.MoveAllToDestPositions(currentCardGameObjects);
    }

    public void RenderAlignment(string alignmentName)
    {
        foreach (var alignment in alignmentPrefabs)
        {
            if (alignment.name == alignmentName)
            {
                RenderAlignment(alignment);
                return;
            }
        }
        
        Debug.LogWarning("Alignment " + alignmentName + " not found!");
    }

    public void RenderAlignment(GameObject alignment)
    {
        Clear();
        var alignmentName = alignment.name;
        currentAlignment = Instantiate(alignment, rootPanel.transform);
        currentAlignment.name = alignmentName;

        var images = currentAlignment.GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            var card = GetRandomCard();
            image.sprite = shirt;
            var userData = image.GetComponent<UserData>();
            userData.card = card;
            userData.cardService = this;
            userData.destPosition = image.transform.position;
            userData.destDegree = image.transform.eulerAngles.z;
        }

        
        foreach (var image in images)
        {
            currentCardGameObjects.Add(image.gameObject);
        }
        
        _transformHelper.MoveAllToDeck(currentCardGameObjects, deck.transform.position);
        _transformHelper.MoveAllToDestPositions(currentCardGameObjects);
    }

    public void OpenDescription(Card card)
    {
        descriptionPopup = Instantiate(descriptionPopupPrefab, rootPanel.transform);
        var content = descriptionPopup.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0);
        var img = content.transform.GetChild(0).gameObject.GetComponent<Image>();
        var title = content.transform.GetChild(1).gameObject.GetComponent<Text>();
        var description = content.transform.GetChild(2).gameObject.GetComponent<Text>();
        var closeButton = descriptionPopup.transform.GetChild(1).gameObject.GetComponent<Image>();

        img.sprite = card.sprite;
        title.text = card.title;
        description.text = card.description;
        
        closeButton.gameObject.AddComponent<ActionBehaviour>();
        closeButton.gameObject.GetComponent<ActionBehaviour>().action = () => Destroy(descriptionPopup);

    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("AlignmentSelection");
    }

    private Card GetRandomCard()
    {
        var rand = Random.Range(0, cards.Count);
        var result = cards[rand];

        if (usedCards.Contains(result))
        {
            return GetRandomCard();
        }

        usedCards.Add(result);
        return result;
    }
}