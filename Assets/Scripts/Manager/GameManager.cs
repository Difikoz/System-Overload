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

        private bool _completed;

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
            WaitForSeconds delay = new(0.25f);
            yield return null;
            _playerInput = GetComponentInChildren<PlayerInputManager>();
            _playerCamera = GetComponentInChildren<PlayerCameraManager>();
            _playerUI = GetComponentInChildren<PlayerUIManager>();
            _worldLayer = GetComponentInChildren<WorldLayerManager>();
            _worldData = GetComponentInChildren<WorldDataManager>();
            _worldObject = GetComponentInChildren<WorldObjectManager>();
            _worldSaveLoad = GetComponentInChildren<WorldSaveLoadManager>();
            _worldSound = GetComponentInChildren<WorldSoundManager>();
            _worldTime = GetComponentInChildren<WorldTimeManager>();
            yield return null;
            _playerUI.LoadingScreenUI.Show();
            yield return null;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 0, 1);
            _player = LeanPool.Spawn(_worldData.PlayerPrefab).GetComponent<PlayerController>();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 0, 1);
            _playerInput.Initialize();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 0, 1);
            _playerCamera.Initialize();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 0, 1);
            _playerUI.Initialize();
            yield return delay;
            _playerUI.MenuUI.CloseMenu();
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 0, 1);
            _worldData.Initialize();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 0, 1);
            _worldSound.Initialize();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 0, 1);
            _worldTime.Initialize();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 0, 1);
            _worldSaveLoad.LoadGame();
            yield return delay;
            _playerUI.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 1, 1);
            yield return delay;
            _playerUI.LoadingScreenUI.Hide();
            Debug.Log($"Loaded : {Time.timeSinceLevelLoad} seconds.");
            yield return null;
            _playerInput.Enable();
            yield return null;
            _completed = true;
        }

        private void Update()
        {
            if (!_completed)
            {
                return;
            }
            _playerInput.HandleUpdate();
            _worldTime.HandleUpdate();
        }

        private void LateUpdate()
        {
            if (!_completed)
            {
                return;
            }
            _playerCamera.HandleUpdate();
        }
    }
}