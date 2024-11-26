using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private WorldPlayerManager _playerManager;
        private WorldInputManager _inputManager;
        private WorldCameraManager _cameraManager;
        private WorldUIManager _uiManager;
        private WorldLayerManager _layerManager;
        private WorldConfigsManager _dataManager;
        private WorldObjectManager _objectManager;
        private WorldSaveLoadManager _saveLoadManager;
        private WorldSoundManager _soundManager;
        private WorldTimeManager _timeManager;

        private bool _completed;

        public WorldPlayerManager PlayerManager => _playerManager;
        public WorldInputManager InputManager => _inputManager;
        public WorldCameraManager CameraManager => _cameraManager;
        public WorldUIManager UIManager => _uiManager;
        public WorldLayerManager LayerManager => _layerManager;
        public WorldConfigsManager DataManager => _dataManager;
        public WorldObjectManager ObjectManager => _objectManager;
        public WorldSaveLoadManager SaveLoadManager => _saveLoadManager;
        public WorldSoundManager SoundManager => _soundManager;
        public WorldTimeManager TimeManager => _timeManager;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LoadingTimer());
        }

        private IEnumerator LoadingTimer()
        {
            WaitForSeconds delay = new(0.25f);
            yield return null;
            _playerManager = GetComponentInChildren<WorldPlayerManager>();
            _inputManager = GetComponentInChildren<WorldInputManager>();
            _cameraManager = GetComponentInChildren<WorldCameraManager>();
            _uiManager = GetComponentInChildren<WorldUIManager>();
            _layerManager = GetComponentInChildren<WorldLayerManager>();
            _dataManager = GetComponentInChildren<WorldConfigsManager>();
            _objectManager = GetComponentInChildren<WorldObjectManager>();
            _saveLoadManager = GetComponentInChildren<WorldSaveLoadManager>();
            _soundManager = GetComponentInChildren<WorldSoundManager>();
            _timeManager = GetComponentInChildren<WorldTimeManager>();
            yield return null;
            _uiManager.LoadingScreenUI.Show();
            yield return null;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 0, 1);
            _playerManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 0, 1);
            _inputManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 0, 1);
            _cameraManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 0, 1);
            _uiManager.Initialize();
            yield return delay;
            _uiManager.MenuUI.CloseMenu();
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 0, 1);
            _dataManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 0, 1);
            _soundManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 0, 1);
            _timeManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 0, 1);
            _saveLoadManager.LoadGame();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.Hide();
            Debug.Log($"Loaded : {Time.timeSinceLevelLoad} seconds.");
            yield return null;
            _inputManager.Enable();
            yield return null;
            _completed = true;
        }

        private void Update()
        {
            if (!_completed)
            {
                return;
            }
            _timeManager.OnUpdate();
            _playerManager.OnUpdate();
        }

        private void LateUpdate()
        {
            if (!_completed)
            {
                return;
            }
            _cameraManager.OnUpdate();
        }
    }
}