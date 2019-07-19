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

    private readonly List<string> dropdownNames = new List<string>
    {
        "Общий", "На событие"
    };


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
    }

    public void RenderAlignment()
    {
        currentAlignment = Instantiate(alignments[0], rootPanel.transform);

        var images = currentAlignment.GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
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