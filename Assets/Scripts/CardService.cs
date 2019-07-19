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

    private GameObject currentAlignment;
    
    private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();

    private readonly List<string> dropdownNames = new List<string> {"Общий", "На событие"};

    public void Start()
    {
        InitializeCards();
    }

    private void InitializeCards()
    {
        cards.AddRange(GenerateCards(hearts, CardSuit.HEART));
        cards.AddRange(GenerateCards(clubs, CardSuit.CLUB));
        cards.AddRange(GenerateCards(spades, CardSuit.SPADE));
        cards.AddRange(GenerateCards(diamonds, CardSuit.DIAMOND));        
    }

    public void Refresh()
    {
        Clear();
        RenderAlignment();
    }

    public List<string> GetAlignmentNames()
    {
        return dropdownNames;
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
        }
    }
    
    private Card GetRandomCard()
    {
        var rand = Random.Range(0, cards.Count);
        var result = cards[rand];
        usedCards.Add(result);
        return result;
    }
    
    private List<Card> GenerateCards(List<Sprite> sprites, CardSuit suit)
    {
        var cards = new List<Card>();
        
        var values = Enum.GetValues(typeof(CardValue)) as CardValue[];
        var valuesEnum = values.GetEnumerator();
        
        foreach (var sprite in sprites)
        {
            valuesEnum.MoveNext();
            var cardValue = (CardValue) valuesEnum.Current;
            
            cards.Add(new Card(suit, cardValue, sprite));
        }

        return cards;
    }
}