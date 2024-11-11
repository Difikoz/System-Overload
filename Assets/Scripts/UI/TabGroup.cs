using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class TabGroup : MonoBehaviour
    {
        public Sprite BackgroundDeactivated;
        public Sprite BackgroundActivated;
        public List<TabButton> Buttons = new();
        public List<GameObject> Pages = new();

        private TabButton _selectedTab;

        private void Awake()
        {
            OnTabButtonPressed(Buttons[0]);
        }

        public void OnTabButtonPressed(TabButton button)
        {
            if (_selectedTab != null)
            {
                _selectedTab.Deselect();
            }
            _selectedTab = button;
            _selectedTab.OnPressed();
            ResetTabs();
            button.Background.sprite = BackgroundActivated;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < Pages.Count; i++)
            {
                Pages[i].SetActive(i == index);
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in Buttons)
            {
                if (_selectedTab != null && button == _selectedTab)
                {
                    continue;
                }
                button.Background.sprite = BackgroundDeactivated;
            }
        }
    }
}