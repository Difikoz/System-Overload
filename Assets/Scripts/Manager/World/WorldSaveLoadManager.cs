using UnityEngine;

namespace WinterUniverse
{
    public class WorldSaveLoadManager : MonoBehaviour
    {
        [SerializeField] private string _fileName = "SaveData";
        public PawnSaveData CurrentSaveData;

        public void SaveGame()
        {
            GameManager.StaticInstance.Player.SaveData(ref CurrentSaveData);
            DataWriter.CreateSaveFile(CurrentSaveData, _fileName);
        }

        public void LoadGame()
        {
            if (DataWriter.FileExists(_fileName))
            {
                CurrentSaveData = DataWriter.LoadSavedFile(_fileName);
            }
            else
            {
                CurrentSaveData = new();
                GameManager.StaticInstance.Player.CreateCharacter(CurrentSaveData);
                SaveGame();
            }
            GameManager.StaticInstance.Player.LoadData(CurrentSaveData);
        }

        public void DeleteGame()
        {
            //DataWriter.DeleteSavedFile(_fileName);
        }
    }
}