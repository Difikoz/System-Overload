using Lean.Pool;
using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerController _player;
        private PlayerInputManager _playerInput;
        private PlayerCameraManager _playerCamera;
        private PlayerUIManager _playerUI;
        private WorldLayerManager _worldLayer;
        private WorldDataManager _worldData;
        private WorldObjectManager _worldObject;
        private WorldSaveLoadManager _worldSaveLoad;
        private WorldSoundManager _worldSound;
        private WorldTimeManager _worldTime;

        public PlayerController Player => _player;
        public PlayerInputManager PlayerInput => _playerInput;
        public PlayerCameraManager PlayerCamera => _playerCamera;
        public PlayerUIManager PlayerUI => _playerUI;
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
            _playerInput = GetComponentInChildren<PlayerInputManager>();
            yield return null;
            _playerCamera = GetComponentInChildren<PlayerCameraManager>();
            yield return null;
            _playerUI = GetComponentInChildren<PlayerUIManager>();
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
            _playerUI.LoadingScreenUI.Show();
            yield return null;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 0, 1);
            _player = LeanPool.Spawn(_worldData.PlayerPrefab).GetComponent<PlayerController>();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 0, 1);
            _playerInput.Initialize();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 0, 1);
            _playerCamera.Initialize();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 0, 1);
            _playerUI.Initialize();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 0, 1);
            _worldData.Initialize();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 0, 1);
            _worldSound.Initialize();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 0, 1);
            _worldSaveLoad.LoadGame();
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 1, 1);
            yield return new WaitForSeconds(0.25f);
            _playerUI.LoadingScreenUI.Hide();
            //_mainMenu.OpenMainMenuWindow();
            Debug.Log($"Loaded : {Time.timeSinceLevelLoad} seconds.");
        }
    }
}