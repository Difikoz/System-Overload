using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private MainMenuManager _mainMenu;
        private PlayerController _player;
        private PlayerInputManager _playerInput;
        private PlayerCameraManager _playerCamera;
        private WorldLayerManager _worldLayer;
        private WorldDataManager _worldData;
        private WorldObjectManager _worldObject;
        private WorldSaveLoadManager _worldSaveLoad;
        private WorldSoundManager _worldSound;
        private WorldTimeManager _worldTime;

        public MainMenuManager MainMenu => _mainMenu;
        public PlayerController Player => _player;
        public PlayerInputManager PlayerInput => _playerInput;
        public PlayerCameraManager PlayerCamera => _playerCamera;
        public WorldLayerManager WorldLayer => _worldLayer;
        public WorldDataManager WorldData => _worldData;
        public WorldObjectManager WorldObject => _worldObject;
        public WorldSaveLoadManager WorldSaveLoad => _worldSaveLoad;
        public WorldSoundManager WorldSound => _worldSound;
        public WorldTimeManager WorldTime => _worldTime;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LoadingTimer());
        }

        private IEnumerator LoadingTimer()
        {
            yield return null;
            _mainMenu = GetComponentInChildren<MainMenuManager>();
            yield return null;
            _playerInput = GetComponentInChildren<PlayerInputManager>();
            yield return null;
            _playerCamera = GetComponentInChildren<PlayerCameraManager>();
            yield return null;
            _worldLayer = GetComponentInChildren<WorldLayerManager>();
            yield return null;
            _worldData = GetComponentInChildren<WorldDataManager>();
            yield return null;
            _worldObject = GetComponentInChildren<WorldObjectManager>();
            yield return null;
            _worldSaveLoad = GetComponentInChildren<WorldSaveLoadManager>();
            yield return null;
            _worldSound = GetComponentInChildren<WorldSoundManager>();
            yield return null;
            _worldTime = GetComponentInChildren<WorldTimeManager>();
            yield return null;
            _mainMenu.Initialize();
            yield return null;
            _playerInput.Initialize();
            yield return null;
            _playerCamera.Initialize();
            yield return null;
            _worldData.Initialize();
            yield return null;
            _worldSound.Initialize();
            yield return null;
            _player = LeanPool.Spawn(_worldData.PlayerPrefab).GetComponent<PlayerController>();
            yield return null;
            _worldSaveLoad.LoadGame();
            yield return null;
            // complete
        }
    }
}