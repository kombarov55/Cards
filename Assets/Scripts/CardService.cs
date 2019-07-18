using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardService : MonoBehaviour
{

    public List<Image> cardImages;
    public List<Sprite> hearts;
    public List<Sprite> clubs;
    public List<Sprite> spades;
    public List<Sprite> diamonds;

    public GameObject alignment;
    public GameObject rootPanel;

    public void Refresh()
    {
        Clear();
        RenderAlignment();
    }

    public void Clear()
    {
        Destroy(alignment);
    }

    public void RenderAlignment()
    {
        Instantiate(alignment, rootPanel.transform);
        
        var images = alignment.GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            Instantiate(image, alignment.transform);
            var randomSprite = GetRandomSprite();
            image.sprite = randomSprite;
            var userData = image.GetComponent<UserData>();
            userData.description = "КОРОЛЬ МЕЧЕЙ!";
        }
    }
    
    private Sprite GetRandomSprite()
    {
        var range = Random.Range(0, 4);
        switch (range)
        {
            case 0:
                return hearts[Random.Range(0, hearts.Count)];
            case 1:
                return clubs[Random.Range(0, clubs.Count)];
            case 2:
                return spades[Random.Range(0, spades.Count)];
            case 3:
                return diamonds[Random.Range(0, diamonds.Count)];
            
            default: throw new Exception("Random.range(0, 4) returned " + range + ". impossible.");
        }
    }
}