using UnityEngine;

namespace WinterUniverse
{
    public abstract class WeaponActionConfig : ScriptableObject
    {
        public abstract void AttempToPerformAction(PawnController pawn);
    }
}