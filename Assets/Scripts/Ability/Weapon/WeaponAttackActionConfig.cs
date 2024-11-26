using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Attack Action", menuName = "Winter Universe/Action/Weapon/New Attack")]
    public class WeaponAttackActionConfig : WeaponActionConfig
    {
        public override void AttempToPerformAction(PawnController pawn)
        {
            if (pawn.IsPerfomingAction || pawn.IsDead)
            {
                return;
            }
            pawn.PawnAnimator.PlayActionAnimation($"Attack");
        }
    }
}