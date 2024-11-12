using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Attack Action", menuName = "Winter Universe/Action/Weapon/New Attack")]
    public class WeaponAttackActionConfig : WeaponActionConfig
    {
        public override void AttempToPerformAction(PawnController pawn)
        {
            pawn.PawnAnimator.PlayActionAnimation($"Attack {pawn.PawnCombat.CurrentAttackType} {pawn.PawnCombat.CurrentSlotType}", true);
        }
    }
}