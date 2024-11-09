using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerController _player;
        private CameraManager _camera;
        private LayerManager _layer;

        public PlayerController Player => _player;
        public CameraManager Camera => _camera;
        public LayerManager Layer => _layer;

        public void SetPlayer(PlayerController player)
        {
            _player = player;
        }
    }
}