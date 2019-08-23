using System.Collections;
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
    public GameObject trashDeck;

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
        StartCoroutine(ClearCoroutine(currentCardGameObjects));

//        if (currentAlignment != null)
//        {
//            Destroy(currentAlignment);
//        }
//
//        usedCards.Clear();
//        currentCardGameObjects.Clear();
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

    public bool IsAlignmentRendered()
    {
        return currentCardGameObjects.Count != 0;
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

    private IEnumerator ClearCoroutine(List<GameObject> currentCardGameObjects)
    {
        List<Text> allTexts = new List<Text>(currentAlignment.gameObject.GetComponentsInChildren<Text>()); 
        
        yield return _transformHelper.ClearCoroutine(currentCardGameObjects, allTexts);
        if (currentAlignment != null)
        {
            Destroy(currentAlignment);
        }

        usedCards.Clear();
        currentCardGameObjects.Clear();
    }
}