using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class WorldSaveGameManager : Singleton<WorldSaveGameManager>
    {
        [HideInInspector] public PlayerController Player;

        public CharacterSaveData CurrentSaveData;

        private string _currentSaveFileName;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SaveGame()
        {
            SaveGame(_currentSaveFileName);
        }

        public void SaveGame(string fileName)
        {
            Player.SaveData(ref CurrentSaveData);
            DataWriter.CreateSaveFile(CurrentSaveData, fileName);
        }

        public void LoadGame(MainMenuSaveSlot slot)
        {
            _currentSaveFileName = slot.FileName;
            CurrentSaveData = slot.CurrentSaveData;
            StartCoroutine(LoadWorldScene());
        }

        public void DeleteGame(string fileName)
        {
            DataWriter.DeleteSavedFile(fileName);
        }

        private IEnumerator LoadWorldScene()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);
            Player.LoadData(CurrentSaveData);
            yield return null;
        }
    }
}