using UnityEngine;

namespace WinterUniverse
{
    public class PlayerLocomotionModule : LocomotionModule
    {
        private PlayerController _player;

        protected override void Awake()
        {
            base.Awake();
            _player = GetComponent<PlayerController>();
        }

        protected override Vector2 GetMoveInput()
        {
            return PlayerInputManager.StaticInstance.MoveInput;
        }

        protected override Vector3 GetLookDirection()
        {
            return CameraManager.StaticInstance.transform.forward;
        }
    }
}