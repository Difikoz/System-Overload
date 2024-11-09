using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WinterUniverse
{
    public class MainMenuSaveSlot : MonoBehaviour, IPointerDownHandler, ISelectHandler, ISubmitHandler
    {
        public string FileName = "SaveData01";
        public CharacterSaveData CurrentSaveData;
        [HideInInspector] public bool DataExists;

        [SerializeField] private TextMeshProUGUI _buttonText;

        private void OnEnable()
        {
            LoadSaveSlot();
        }

        private void LoadSaveSlot()
        {
            if (DataWriter.FileExists(FileName))
            {
                CurrentSaveData = DataWriter.LoadSavedFile(FileName);
                _buttonText.text = CurrentSaveData.CharacterName;
                DataExists = true;
            }
            else
            {
                CurrentSaveData = new();
                _buttonText.text = "Empty";
                DataExists = false;
            }
        }

        private void OnButtonPressed()
        {
            MainMenuManager.StaticInstance.OnSaveSlotPressed();
        }

        public void OnSelect(BaseEventData eventData)
        {
            MainMenuManager.StaticInstance.SelectSaveSlot(this);
            if (DataExists)
            {
                GameManager.StaticInstance.Player.LoadData(CurrentSaveData);
                // show player if data exists
            }
            else
            {
                GameManager.StaticInstance.Player.ClearCharacter();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnButtonPressed();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            OnButtonPressed();
        }
    }
}