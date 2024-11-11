using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler, ISubmitHandler
    {
        public TabGroup Group;
        public Image Background;

        public void OnPointerClick(PointerEventData eventData)
        {
            Group.OnTabButtonPressed(this);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Group.OnTabButtonPressed(this);
        }

        public void OnPressed()
        {
            
        }

        public void Deselect()
        {
            
        }
    }
}