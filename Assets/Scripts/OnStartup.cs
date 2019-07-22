using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class OnStartup : MonoBehaviour
    {

        public CardService cardService;
        public Dropdown dropdown;
    
        void Start()
        {
            
            cardService.LoadCards();
            
            var alignmentNames = cardService.GetAlignmentNames();
            dropdown.options.Clear();
            foreach (var name in alignmentNames)
            {
                dropdown.options.Add(new Dropdown.OptionData(name));
            }
            
            dropdown.onValueChanged.AddListener((index) =>
            {
                var name = dropdown.options[index].text;
                cardService.RenderAlignment(name);
            });

                cardService.RenderAlignment(alignmentNames[0]);
        }
    }
}