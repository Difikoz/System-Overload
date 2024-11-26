using UnityEngine;

namespace WinterUniverse
{
    public static class ExtraTools
    {
        public static float GetSignedAngleToDirection(Vector3 forward, Vector3 direction)
        {
            direction.y = 0f;
            return Vector3.SignedAngle(forward, direction.normalized, Vector3.up);
        }
    }
}