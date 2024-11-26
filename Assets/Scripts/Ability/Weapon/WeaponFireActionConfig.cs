using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Fire Action", menuName = "Winter Universe/Action/Weapon/New Fire")]
    public class WeaponFireActionConfig : WeaponActionConfig
    {
        public override void AttempToPerformAction(PawnController pawn)
        {
            if (pawn.IsPerfomingAction || pawn.IsDead)
            {
                return;
            }
            pawn.PawnAnimator.PlayActionAnimation($"Fire", false, 0f, true, true);
        }
    }
}