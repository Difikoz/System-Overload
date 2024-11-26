using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Attack Action", menuName = "Winter Universe/Action/AI/New Attack")]
    public class AIPrimaryAttackActionConfig : AIActionConfig
    {
        public AttackType Type;
        public HandSlotType Slot;

        public override void AttempToPerformAction(AIController ai)
        {
            if (Slot == HandSlotType.Right)
            {
                ai.Pawn.PawnCombat.UseWeaponAction(ai.Pawn.PawnEquipment.WeaponRightSlot.Config, Type, Slot);
            }
            else
            {
                ai.Pawn.PawnCombat.UseWeaponAction(ai.Pawn.PawnEquipment.WeaponLeftSlot.Config, Type, Slot);
            }
        }

        public override bool InRangeToUse(AIController ai)
        {
            if (Slot == HandSlotType.Right)
            {
                MinDistance = ai.Pawn.PawnEquipment.WeaponRightSlot.Config.MinDistance;
                MaxDistance = ai.Pawn.PawnEquipment.WeaponRightSlot.Config.MaxDistance;
            }
            else
            {
                MinDistance = ai.Pawn.PawnEquipment.WeaponLeftSlot.Config.MinDistance;
                MaxDistance = ai.Pawn.PawnEquipment.WeaponLeftSlot.Config.MaxDistance;
            }
            return base.InRangeToUse(ai);
        }

        public override bool InAngleToUse(AIController ai)
        {
            if (Slot == HandSlotType.Right)
            {
                Angle = ai.Pawn.PawnEquipment.WeaponRightSlot.Config.Angle;
            }
            else
            {
                Angle = ai.Pawn.PawnEquipment.WeaponLeftSlot.Config.Angle;
            }
            return base.InAngleToUse(ai);
        }
    }
}