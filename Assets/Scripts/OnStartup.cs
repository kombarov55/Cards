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
            cardService.Refresh();
            
            dropdown.options.Clear();
            
            foreach (var name in cardService.GetAlignmentNames())
            {
                dropdown.options.Add(new Dropdown.OptionData(name));
            }
        }
    }
}