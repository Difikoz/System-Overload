using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon Action", menuName = "Winter Universe/Action/AI/New Weapon Action")]
    public class AIWeaponActionConfig : AIActionConfig
    {
        public override void AttempToPerformAction(AIController ai)
        {
            ai.Pawn.PawnEquipment.WeaponSlot.Config.Action.AttempToPerformAction(ai.Pawn);
        }

        public override bool InRangeToUse(AIController ai)
        {
            MinDistance = ai.Pawn.PawnEquipment.WeaponSlot.Config.MinDistance;
            MaxDistance = ai.Pawn.PawnEquipment.WeaponSlot.Config.MaxDistance;
            return base.InRangeToUse(ai);
        }

        public override bool InAngleToUse(AIController ai)
        {
            Angle = ai.Pawn.PawnEquipment.WeaponSlot.Config.Angle;
            return base.InAngleToUse(ai);
        }
    }
}