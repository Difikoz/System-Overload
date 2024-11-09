using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MainMenuCharacterCreationManager : Singleton<MainMenuCharacterCreationManager>
    {
        public CharacterSaveData CurrentCharacterData;
        [Header("Race Menu")]
        [SerializeField] private GameObject _raceMenuWindow;
        [SerializeField] private Button _raceMenuButtonBack;
        [SerializeField] private Button _raceMenuButtonPreviousRace;
        [SerializeField] private Button _raceMenuButtonNextRace;
        [SerializeField] private Button _raceMenuButtonConfirm;
        [SerializeField] private TextMeshProUGUI _raceNameText;
        [SerializeField] private TextMeshProUGUI _raceDescriptionText;
        [SerializeField] private TextMeshProUGUI _raceStatsText;
        [SerializeField] private List<RaceData> _races = new();
        private int _currentRaceIndex;
        [Header("Gender Menu")]
        [SerializeField] private GameObject _genderMenuWindow;
        [SerializeField] private Button _genderMenuButtonBack;
        [SerializeField] private Button _genderMenuButtonMale;
        [SerializeField] private Button _genderMenuButtonFemale;
        [SerializeField] private Button _genderMenuButtonConfirm;
        [SerializeField] private TextMeshProUGUI _genderNameText;
        [SerializeField] private List<Gender> _genders = new();
        [Header("Faction Menu")]
        [SerializeField] private GameObject _factionMenuWindow;
        [SerializeField] private Button _factionMenuButtonBack;
        [SerializeField] private Button _factionMenuButtonPreviousFaction;
        [SerializeField] private Button _factionMenuButtonNextFaction;
        [SerializeField] private Button _factionMenuButtonConfirm;
        [SerializeField] private TextMeshProUGUI _factionNameText;
        [SerializeField] private TextMeshProUGUI _factionDescriptionText;
        [SerializeField] private List<FactionData> _factions = new();
        private int _currentFactionIndex;
        [Header("Complete Menu")]
        [SerializeField] private GameObject _completeMenuWindow;
        [SerializeField] private Button _completeMenuButtonConfirm;
        [SerializeField] private Button _completeMenuButtonBack;

        private void OnEnable()
        {
            // race menu
            _raceMenuButtonBack.onClick.AddListener(OnRaceMenuButtonBackPressed);
            _raceMenuButtonPreviousRace.onClick.AddListener(OnRaceMenuButtonPreviousRacePressed);
            _raceMenuButtonNextRace.onClick.AddListener(OnRaceMenuButtonNextRacePressed);
            _raceMenuButtonConfirm.onClick.AddListener(OnRaceMenuButtonConfirmPressed);
            // gender menu
            _genderMenuButtonBack.onClick.AddListener(OnGenderMenuButtonBackPressed);
            _genderMenuButtonMale.onClick.AddListener(OnGenderMenuButtonMalePressed);
            _genderMenuButtonFemale.onClick.AddListener(OnGenderMenuButtonFemalePressed);
            _genderMenuButtonConfirm.onClick.AddListener(OnGenderMenuButtonConfirmPressed);
            // faction menu
            _factionMenuButtonBack.onClick.AddListener(OnFactionMenuButtonBackPressed);
            _factionMenuButtonPreviousFaction.onClick.AddListener(OnFactionMenuButtonPreviousFactionPressed);
            _factionMenuButtonNextFaction.onClick.AddListener(OnFactionMenuButtonNextFactionPressed);
            _factionMenuButtonConfirm.onClick.AddListener(OnFactionMenuButtonConfirmPressed);
            // complete menu
            _completeMenuButtonConfirm.onClick.AddListener(OnCompleteMenuButtonConfirmPressed);
            _completeMenuButtonBack.onClick.AddListener(OnCompleteMenuButtonBackPressed);
            SelectRace();
            CurrentCharacterData.Gender = Gender.Male.ToString();
            _genderNameText.text = Gender.Male.ToString();
            SelectFaction();
        }

        private void OnDisable()
        {
            // race menu
            _raceMenuButtonBack.onClick.RemoveListener(OnRaceMenuButtonBackPressed);
            // gender menu
            _genderMenuButtonBack.onClick.RemoveListener(OnGenderMenuButtonBackPressed);
            // faction menu
            _factionMenuButtonBack.onClick.RemoveListener(OnFactionMenuButtonBackPressed);
            // complete menu
            _completeMenuButtonConfirm.onClick.RemoveListener(OnCompleteMenuButtonConfirmPressed);
            _completeMenuButtonBack.onClick.RemoveListener(OnCompleteMenuButtonBackPressed);
        }

        private void OnRaceMenuButtonBackPressed()
        {
            // пока что это выход в меню старта игры.
        }

        private void OnRaceMenuButtonPreviousRacePressed()
        {
            if (_currentRaceIndex == 0)
            {
                _currentRaceIndex = _races.Count - 1;
            }
            else
            {
                _currentRaceIndex--;
            }
            SelectRace();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnRaceMenuButtonNextRacePressed()
        {
            if (_currentRaceIndex == _races.Count - 1)
            {
                _currentRaceIndex = 0;
            }
            else
            {
                _currentRaceIndex++;
            }
            SelectRace();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnRaceMenuButtonConfirmPressed()
        {
            _raceMenuWindow.SetActive(false);
            _genderMenuWindow.SetActive(true);
            _genderMenuButtonBack.Select();
        }

        private void SelectRace()
        {
            CurrentCharacterData.Race = _races[_currentRaceIndex].DisplayName;
            _raceNameText.text = _races[_currentRaceIndex].DisplayName;
            _raceDescriptionText.text = _races[_currentRaceIndex].Description;
            _raceStatsText.text = "Stat Modifiers:";
            foreach (StatModifierCreator creator in _races[_currentRaceIndex].Modifiers)
            {
                _raceStatsText.text += $"\n{creator.Stat.DisplayName} [{creator.Modifier.Value}{(creator.Stat.IsPercent || creator.Modifier.Type == StatModifierType.Multiplier ? "%" : "")}]";
            }
        }

        private void OnGenderMenuButtonBackPressed()
        {
            _genderMenuWindow.SetActive(false);
            _raceMenuWindow.SetActive(true);
            _raceMenuButtonBack.Select();
        }

        private void OnGenderMenuButtonMalePressed()
        {
            CurrentCharacterData.Gender = Gender.Male.ToString();
            _genderNameText.text = Gender.Male.ToString();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnGenderMenuButtonFemalePressed()
        {
            CurrentCharacterData.Gender = Gender.Female.ToString();
            _genderNameText.text = Gender.Female.ToString();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnGenderMenuButtonConfirmPressed()
        {
            _genderMenuWindow.SetActive(false);
            _factionMenuWindow.SetActive(true);
            _factionMenuButtonBack.Select();
        }

        private void OnFactionMenuButtonBackPressed()
        {
            _factionMenuWindow.SetActive(false);
            _genderMenuWindow.SetActive(true);
            _genderMenuButtonBack.Select();
        }

        private void OnFactionMenuButtonPreviousFactionPressed()
        {
            if (_currentFactionIndex == 0)
            {
                _currentFactionIndex = _factions.Count - 1;
            }
            else
            {
                _currentFactionIndex--;
            }
            SelectFaction();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnFactionMenuButtonNextFactionPressed()
        {
            if (_currentFactionIndex == _factions.Count - 1)
            {
                _currentFactionIndex = 0;
            }
            else
            {
                _currentFactionIndex++;
            }
            SelectFaction();
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);
        }

        private void OnFactionMenuButtonConfirmPressed()
        {
            _factionMenuWindow.SetActive(false);
            // TODO first open starting items selecting
            _completeMenuWindow.SetActive(true);
            _completeMenuButtonBack.Select();
        }

        private void SelectFaction()
        {
            CurrentCharacterData.Faction = _factions[_currentFactionIndex].DisplayName;
            _factionNameText.text = _factions[_currentFactionIndex].DisplayName;
            _factionDescriptionText.text = _factions[_currentFactionIndex].Description;
        }

        private void OnCompleteMenuButtonConfirmPressed()
        {
            _completeMenuWindow.SetActive(false);
            _raceMenuWindow.SetActive(true);// включить самое первое окно для последующих созданий персонажа
            // менеджер главного меню сам выберет кнопку
        }

        private void OnCompleteMenuButtonBackPressed()
        {
            _completeMenuWindow.SetActive(false);
            _factionMenuWindow.SetActive(true);
            _factionMenuButtonBack.Select();
        }

        public void SelectStartingItems(List<ItemStack> stacks)
        {
            // TODO
            CurrentCharacterData.InventoryStacks.Clear();
            foreach (ItemStack stack in stacks)
            {
                CurrentCharacterData.InventoryStacks.Add(stack.Item.DisplayName, stack.Amount);
            }
            // TODO move to complete window
            PlayerInputManager.StaticInstance.Player.CreateCharacter(CurrentCharacterData);// TODO ???
        }
    }
}