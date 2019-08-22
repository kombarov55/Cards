using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public enum AlignmentType
    {
        PlayingCards,
        Runes
    }

    public class ListAlignmentsScript : MonoBehaviour
    {
        public GameObject buttonPrefab;
        public GameObject scrollviewPrefab;

        public List<GameObject> playingCardAlignmentPrefabs;
        public List<GameObject> runeAlignmentPrefabs;

        private GameObject instantiatedScrollview;


        public void ListRunes()
        {
            ListAlignmentNames(AlignmentType.Runes);
        }

        public void ListPlayingCards()
        {
            ListAlignmentNames(AlignmentType.PlayingCards);
        }
        
        private void ListAlignmentNames(AlignmentType alignmentType)
        {
            changeActiveView();
            addAlignmentNameButtons(alignmentType);
        }

        private void changeActiveView()
        {
            var viewToRemove = transform.parent.GetChild(0).gameObject;
            Destroy(viewToRemove);
            instantiatedScrollview = Instantiate(scrollviewPrefab, transform.parent);
        }

        private void addAlignmentNameButtons(AlignmentType alignmentType)
        {
            List<GameObject> chosenAlignment = null;

            switch (alignmentType)
            {
                case AlignmentType.PlayingCards:
                    chosenAlignment = playingCardAlignmentPrefabs;
                    break;
                case AlignmentType.Runes:
                    chosenAlignment = runeAlignmentPrefabs;
                    break;
            }

            var contentRect = instantiatedScrollview.transform.GetChild(0).GetChild(0).GetChild(0);

            foreach (var prefab in chosenAlignment)
            {
                var prefabName = prefab.name;
                var item = Instantiate(buttonPrefab, contentRect);
                item.transform.GetChild(0).gameObject.GetComponent<Text>().text = prefabName;
            }
        }
    }
}