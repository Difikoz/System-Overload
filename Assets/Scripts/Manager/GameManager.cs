using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerController _player;
        private PlayerCameraManager _playerCamera;
        private WorldLayerManager _worldLayer;
        private WorldDataManager _worldData;
        private WorldObjectManager _worldObject;
        private WorldSaveGameManager _worldSaveGame;
        private WorldSoundManager _worldSound;
        private WorldTimeManager _worldTime;

        public PlayerController Player => _player;
        public PlayerCameraManager PlayerCamera => _playerCamera;
        public WorldLayerManager WorldLayer => _worldLayer;
        public WorldDataManager WorldData => _worldData;
        public WorldObjectManager WorldObject => _worldObject;
        public WorldSaveGameManager WorldSaveGame => _worldSaveGame;
        public WorldSoundManager WorldSound => _worldSound;
        public WorldTimeManager WorldTime => _worldTime;

        protected override void Awake()
        {
            base.Awake();
            _playerCamera = GetComponentInChildren<PlayerCameraManager>();
            _worldLayer = GetComponentInChildren<WorldLayerManager>();
            _worldData = GetComponentInChildren<WorldDataManager>();
            _worldObject = GetComponentInChildren<WorldObjectManager>();
            _worldSaveGame = GetComponentInChildren<WorldSaveGameManager>();
            _worldSound = GetComponentInChildren<WorldSoundManager>();
            _worldTime = GetComponentInChildren<WorldTimeManager>();
            _playerCamera.Initialize();
            _worldData.Initialize();
            _worldTime.Initialize();
        }

        public void SetPlayer(PlayerController player)
        {
            _player = player;
        }
    }
}