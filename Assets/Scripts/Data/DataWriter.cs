using UnityEngine;
using System;
using System.IO;

namespace WinterUniverse
{
    public static class DataWriter
    {
        public static bool FileExists(string fileName)
        {
            return File.Exists(Path.Combine(Application.persistentDataPath, fileName));
        }

        public static void DeleteSavedFile(string fileName)
        {
            if (FileExists(fileName))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, fileName));
            }
        }

        public static void CreateSaveFile(CharacterSaveData characterData, string fileName)
        {
            string savePath = Path.Combine(Application.persistentDataPath, fileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                string data = JsonUtility.ToJson(characterData, true);
                using (FileStream stream = new(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new(stream))
                    {
                        fileWriter.Write(data);
                    }
                }
                //Debug.Log("Data saved to: " + savePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while saving!\n" + e);
            }
        }

        public static CharacterSaveData LoadSavedFile(string fileName)
        {
            CharacterSaveData data = null;
            string loadPath = Path.Combine(Application.persistentDataPath, fileName);
            if (FileExists(fileName))
            {
                try
                {
                    string dataText;
                    using (FileStream stream = new(loadPath, FileMode.Open))
                    {
                        using (StreamReader fileReader = new(stream))
                        {
                            dataText = fileReader.ReadToEnd();
                        }
                    }
                    data = JsonUtility.FromJson<CharacterSaveData>(dataText);
                }
                catch (Exception e)
                {
                    data = default;
                    Debug.LogError("Error while loading!\n" + e);
                }
            }
            return data;
        }
    }
}