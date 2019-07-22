using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class CardService : MonoBehaviour
{

    public List<Sprite> hearts;
    public List<Sprite> clubs;
    public List<Sprite> spades;
    public List<Sprite> diamonds; 

    public List<GameObject> alignments;
    public GameObject rootPanel;
    public GameObject descriptionPopupPrefab;

    private GameObject currentAlignment;
    private GameObject descriptionPopup;
    
    private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();

    public void Start()
    {
        currentAlignment = Instantiate(alignments[0]);
    }

    public void LoadCards()
    {
        cards = CardLoader.LoadCards(hearts, clubs, spades, diamonds);        
    }
    
    public void Refresh()
    {
        var currentAlignmentName = currentAlignment.name;
        RenderAlignment(currentAlignmentName);
    }
    
    public void Clear()
    {
        Destroy(currentAlignment);
        usedCards.Clear();
    }

    public void RenderAlignment(string alignmentName)
    {
        foreach (var alignment in alignments)
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
            image.sprite = card.sprite;
            var userData = image.GetComponent<UserData>();
            userData.card = card;
            var openDescriptionScript = image.GetComponent<OpenDescriptionScript>();
            openDescriptionScript.CardService = this;
        }
    }

    public List<string> GetAlignmentNames()
    {
        List<string> dropdownNames = new List<string>();
        
        foreach (var alignment in alignments)
        {
            dropdownNames.Add(alignment.name);
        }

        return dropdownNames;
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