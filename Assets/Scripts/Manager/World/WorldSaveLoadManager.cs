using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class WorldSaveLoadManager : MonoBehaviour
    {
        public CharacterSaveData CurrentSaveData;

        private string _currentSaveFileName;

        public void SaveGame()
        {
            SaveGame(_currentSaveFileName);
        }

        public void SaveGame(string fileName)
        {
            GameManager.StaticInstance.Player.SaveData(ref CurrentSaveData);
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
            GameManager.StaticInstance.Player.LoadData(CurrentSaveData);
            yield return null;
        }
    }
}