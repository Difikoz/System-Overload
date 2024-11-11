using UnityEngine;

namespace WinterUniverse
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _menuWindow;

        public void Initialize()
        {
            OpenMenu();
        }

        public void OpenMenu()
        {
            _menuWindow.SetActive(true);
        }

        public void CloseMenu()
        {
            _menuWindow.SetActive(false);
        }
    }
}