using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        public GameObject alignmentTypeSelectionPrefab;

        public List<GameObject> playingCardAlignmentPrefabs;
        public List<GameObject> runeAlignmentPrefabs;
        
        private GameObject instantiatedScrollview;
        private GameObject instantiatedAlignementTypeSelection;

        public void Start()
        {
            instantiatedAlignementTypeSelection = Instantiate(alignmentTypeSelectionPrefab, transform.parent);
            addButtonsOnclicks(instantiatedAlignementTypeSelection);
        }
        

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
            Destroy(instantiatedAlignementTypeSelection);
            instantiatedScrollview = Instantiate(scrollviewPrefab, transform.parent);

            var button = instantiatedScrollview.transform.GetChild(1).gameObject;
            button.AddComponent<ActionBehaviour>();
            button.GetComponent<ActionBehaviour>().action = () => { changeViewBack(); };
        }
        
        private void changeViewBack()
        {
            Destroy(instantiatedScrollview);
            instantiatedAlignementTypeSelection = Instantiate(alignmentTypeSelectionPrefab, transform.parent);
            addButtonsOnclicks(instantiatedAlignementTypeSelection);
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

        private void addButtonsOnclicks(GameObject instantiatedAlignmentTypeSelection)
        {
            var playingCardsBtn = instantiatedAlignmentTypeSelection.transform.GetChild(0);
            var runesBtn = instantiatedAlignmentTypeSelection.transform.GetChild(1);
            
            playingCardsBtn.gameObject.AddComponent<ActionBehaviour>();
            playingCardsBtn.gameObject.GetComponent<ActionBehaviour>().action = () => { ListPlayingCards(); };
            
            runesBtn.gameObject.AddComponent<ActionBehaviour>();
            runesBtn.gameObject.GetComponent<ActionBehaviour>().action = () => { ListRunes(); };            
        }
    }
}