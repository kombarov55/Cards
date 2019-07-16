using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OnStartup : MonoBehaviour
{

    public List<GameObject> cardSprites;
    public List<Sprite> hearts;
    public List<Sprite> clubs;
    public List<Sprite> spades;
    public List<Sprite> diamonds;
    
    void Start()
    {
        /*
         * По каждой из карт:
         *
         * 1. Поменять рисунок на случайный
         */
        foreach (var sprite in cardSprites)
        {
        
            var instantiate = Instantiate(sprite);
            var randomSprite = getRandomSprite();
            instantiate.GetComponent<SpriteRenderer>().sprite = randomSprite;
        }
    }
    
    private Sprite getRandomSprite()
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