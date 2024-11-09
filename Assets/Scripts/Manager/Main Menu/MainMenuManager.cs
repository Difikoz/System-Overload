using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuManager : Singleton<MainMenuManager>
    {
        [Header("Title Screen")]
        [SerializeField] private GameObject _titleScreenWindow;
        [SerializeField] private Button _titleScreenButtonStart;
        [Header("Main Menu")]
        [SerializeField] private GameObject _mainMenuWindow;
        [SerializeField] private Button _mainMenuButtonStartGame;
        [SerializeField] private Button _mainMenuButtonSettings;
        [SerializeField] private Button _mainMenuButtonQuitGame;
        [Header("Start Game Menu")]
        [SerializeField] private GameObject _startGameMenuWindow;
        [SerializeField] private Button _startGameMenuButtonBack;
        [Header("Character Creation Menu")]
        [SerializeField] private GameObject _characterCreationMenuWindow;
        [SerializeField] private Button _characterCreationMenuButtonCreate;
        [SerializeField] private Button _characterCreationMenuButtonBack;
        [Header("Settings Menu")]
        [SerializeField] private GameObject _settingsMenuWindow;
        [SerializeField] private Button _settingsMenuButtonBack;
        [Header("Notifications")]
        [SerializeField] private GameObject _deleteSaveDataNotification;
        [SerializeField] private Button _deleteSaveDataButtonConfirm;
        [SerializeField] private Button _deleteSaveDataButtonBack;

        [HideInInspector] public MainMenuSaveSlot CurrentSelectedSaveSlot;

        private void OnEnable()
        {
            _titleScreenButtonStart.Select();
            // title screen
            _titleScreenButtonStart.onClick.AddListener(OnTitleScreenButtonStartPressed);
            // main menu
            _mainMenuButtonStartGame.onClick.AddListener(OnMainMenuButtonStartGamePressed);
            _mainMenuButtonSettings.onClick.AddListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.AddListener(OnainMenuButtonQuitGamePressed);
            // start game menu
            _startGameMenuButtonBack.onClick.AddListener(OnStartGameMenuButtonBackPressed);
            // character creation
            _characterCreationMenuButtonCreate.onClick.AddListener(OnCharacterCreationMenuButtonCreatePressed);
            _characterCreationMenuButtonBack.onClick.AddListener(OnCharacterCreationMenuButtonBackPressed);
            // settings menu
            _settingsMenuButtonBack.onClick.AddListener(OnSettingsMenuButtonBackPressed);
            // notifications
            _deleteSaveDataButtonConfirm.onClick.AddListener(OnDeleteSaveDataButtonConfirmPressed);
            _deleteSaveDataButtonBack.onClick.AddListener(OnDeleteSaveDataButtonBackPressed);
        }

        private void OnDisable()
        {
            // title screen
            _titleScreenButtonStart.onClick.RemoveListener(OnTitleScreenButtonStartPressed);
            // main menu
            _mainMenuButtonStartGame.onClick.RemoveListener(OnMainMenuButtonStartGamePressed);
            _mainMenuButtonSettings.onClick.RemoveListener(OnMainMenuButtonSettingsPressed);
            _mainMenuButtonQuitGame.onClick.RemoveListener(OnainMenuButtonQuitGamePressed);
            // start game menu
            _startGameMenuButtonBack.onClick.RemoveListener(OnStartGameMenuButtonBackPressed);
            // character creation
            _characterCreationMenuButtonCreate.onClick.RemoveListener(OnCharacterCreationMenuButtonCreatePressed);
            _characterCreationMenuButtonBack.onClick.RemoveListener(OnCharacterCreationMenuButtonBackPressed);
            // settings menu
            _settingsMenuButtonBack.onClick.RemoveListener(OnSettingsMenuButtonBackPressed);
            // notifications
            _deleteSaveDataButtonConfirm.onClick.RemoveListener(OnDeleteSaveDataButtonConfirmPressed);
            _deleteSaveDataButtonBack.onClick.RemoveListener(OnDeleteSaveDataButtonBackPressed);
        }

        private void OnTitleScreenButtonStartPressed()
        {
            _titleScreenWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonStartGame.Select();
            if (PlayerInputManager.StaticInstance.Player == null)
            {
                LeanPool.Spawn(GameManager.StaticInstance.WorldData.PlayerPrefab);
            }
            else
            {
                PlayerInputManager.StaticInstance.Player.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }

        private void OnMainMenuButtonStartGamePressed()
        {
            _mainMenuWindow.SetActive(false);
            _startGameMenuWindow.SetActive(true);
            _startGameMenuButtonBack.Select();
        }

        private void OnMainMenuButtonSettingsPressed()
        {
            _mainMenuWindow.SetActive(false);
            _settingsMenuWindow.SetActive(true);
            _settingsMenuButtonBack.Select();
        }

        private void OnainMenuButtonQuitGamePressed()
        {
            Application.Quit();
        }

        private void OnStartGameMenuButtonBackPressed()
        {
            _startGameMenuWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonStartGame.Select();
        }

        private void OnCharacterCreationMenuButtonCreatePressed()
        {
            GameManager.StaticInstance.WorldSaveGame.SaveGame(CurrentSelectedSaveSlot.FileName);
            _characterCreationMenuWindow.SetActive(false);
            _startGameMenuWindow.SetActive(true);
            _startGameMenuButtonBack.Select();
            PlayerInputManager.StaticInstance.Player.ClearCharacter();
        }

        private void OnCharacterCreationMenuButtonBackPressed()
        {
            _characterCreationMenuWindow.SetActive(false);
            _startGameMenuWindow.SetActive(true);
            _startGameMenuButtonBack.Select();
            PlayerInputManager.StaticInstance.Player.ClearCharacter();
        }

        private void OnSettingsMenuButtonBackPressed()
        {
            _settingsMenuWindow.SetActive(false);
            _mainMenuWindow.SetActive(true);
            _mainMenuButtonSettings.Select();
        }

        private void OnDeleteSaveDataButtonConfirmPressed()
        {
            if (CurrentSelectedSaveSlot != null && CurrentSelectedSaveSlot.DataExists)
            {
                GameManager.StaticInstance.WorldSaveGame.DeleteGame(CurrentSelectedSaveSlot.FileName);
            }
            _startGameMenuWindow.SetActive(false);
            _startGameMenuWindow.SetActive(true);
            OnDeleteSaveDataButtonBackPressed();
        }

        private void OnDeleteSaveDataButtonBackPressed()
        {
            _deleteSaveDataNotification.SetActive(false);
            _startGameMenuButtonBack.Select();
        }

        private void DisplayCharacterCreationWindow()
        {
            _startGameMenuWindow.SetActive(false);
            _characterCreationMenuWindow.SetActive(true);
            _characterCreationMenuButtonBack.Select();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(MainMenuCharacterCreationManager.StaticInstance.CurrentCharacterData);// TODO ???
        }
        //
        public void OnSaveSlotPressed()
        {
            if (CurrentSelectedSaveSlot == null)
            {
                return;
            }
            if (CurrentSelectedSaveSlot.DataExists)
            {
                GameManager.StaticInstance.WorldSaveGame.LoadGame(CurrentSelectedSaveSlot);
            }
            else
            {
                DisplayCharacterCreationWindow();
            }
        }

        public void SelectSaveSlot(MainMenuSaveSlot slot)
        {
            CurrentSelectedSaveSlot = slot;
        }

        public void OnStartGameMenuButtonBackSelected()
        {
            PlayerInputManager.StaticInstance.Player.ClearCharacter();
            CurrentSelectedSaveSlot = null;
        }

        public void AttempToDeleteSaveData()
        {
            if (CurrentSelectedSaveSlot != null && CurrentSelectedSaveSlot.DataExists)
            {
                _deleteSaveDataNotification.SetActive(true);
                _deleteSaveDataButtonBack.Select();
            }
        }
    }
}