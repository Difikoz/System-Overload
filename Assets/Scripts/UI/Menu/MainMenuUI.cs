using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButtonQuitGame;

        public void Initialize()
        {
            _mainMenuButtonQuitGame.onClick.AddListener(OnainMenuButtonQuitGamePressed);
        }

        private void OnainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }
    }
}