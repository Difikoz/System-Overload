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
                ai.PawnCombat.UseWeaponAction(ai.PawnEquipment.WeaponRightSlot.Config, Type, Slot);
            }
            else
            {
                ai.PawnCombat.UseWeaponAction(ai.PawnEquipment.WeaponLeftSlot.Config, Type, Slot);
            }
        }

        public override bool InRangeToUse(AIController ai)
        {
            if (Slot == HandSlotType.Right)
            {
                MinDistance = ai.PawnEquipment.WeaponRightSlot.Config.MinDistance;
                MaxDistance = ai.PawnEquipment.WeaponRightSlot.Config.MaxDistance;
            }
            else
            {
                MinDistance = ai.PawnEquipment.WeaponLeftSlot.Config.MinDistance;
                MaxDistance = ai.PawnEquipment.WeaponLeftSlot.Config.MaxDistance;
            }
            return base.InRangeToUse(ai);
        }

        public override bool InAngleToUse(AIController ai)
        {
            if (Slot == HandSlotType.Right)
            {
                Angle = ai.PawnEquipment.WeaponRightSlot.Config.Angle;
            }
            else
            {
                Angle = ai.PawnEquipment.WeaponLeftSlot.Config.Angle;
            }
            return base.InAngleToUse(ai);
        }
    }
}