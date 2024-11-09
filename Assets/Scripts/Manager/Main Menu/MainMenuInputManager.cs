using UnityEngine;

namespace WinterUniverse
{
    public class MainMenuInputManager : Singleton<MainMenuInputManager>
    {
        public void OnDelete()
        {
            MainMenuManager.StaticInstance.AttempToDeleteSaveData();
        }
    }
}