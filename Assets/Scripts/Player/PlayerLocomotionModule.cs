using UnityEngine;

namespace WinterUniverse
{
    public class PlayerLocomotionModule : PawnLocomotion
    {
        private PlayerController _player;

        public override void Initialize()
        {
            base.Initialize();
            _player = GetComponent<PlayerController>();
        }

        protected override Vector2 GetMoveInput()
        {
            return GameManager.StaticInstance.PlayerInput.MoveInput;
        }

        protected override Vector3 GetLookDirection()
        {
            return GameManager.StaticInstance.PlayerCamera.transform.forward;
        }
    }
}