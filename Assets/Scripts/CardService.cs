using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    private readonly List<string> dropdownNames = new List<string> {"Общий", "На событие"};

    public void Start()
    {
        cards = CardLoader.LoadCards(hearts, clubs, spades, diamonds);
    }
    
    public void Refresh()
    {
        Clear();
        RenderAlignment();
    }
    
    public void Clear()
    {
        Destroy(currentAlignment);
        usedCards.Clear();
    }
    
    public void RenderAlignment()
    {
        currentAlignment = Instantiate(alignments[0], rootPanel.transform);

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
        usedCards.Add(result);
        return result;
    }
}