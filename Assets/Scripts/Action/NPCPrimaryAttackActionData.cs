using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Primary Attack Action", menuName = "Winter Universe/Character/NPC/Action/New Primary Attack")]
    public class NPCPrimaryAttackActionData : NPCActionData
    {
        public HandSlotType Slot;

        public override void AttempToPerformAction(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.PawnEquipment.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                npc.PawnCombat.UseWeaponAbility(npc.PawnEquipment.WeaponRightSlot.Data, HandSlotType.Right, npc.PawnEquipment.WeaponRightSlot.Data.PrimaryAbility);
            }
            else
            {
                npc.PawnCombat.UseWeaponAbility(npc.PawnEquipment.WeaponLeftSlot.Data, HandSlotType.Left, npc.PawnEquipment.WeaponLeftSlot.Data.PrimaryAbility);
            }
        }

        public override bool InRangeToUse(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.PawnEquipment.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                MinDistance = npc.PawnEquipment.WeaponRightSlot.Data.MinDistance;
                MaxDistance = npc.PawnEquipment.WeaponRightSlot.Data.MaxDistance;
            }
            else
            {
                MinDistance = npc.PawnEquipment.WeaponLeftSlot.Data.MinDistance;
                MaxDistance = npc.PawnEquipment.WeaponLeftSlot.Data.MaxDistance;
            }
            return base.InRangeToUse(npc);
        }

        public override bool InAngleToUse(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.PawnEquipment.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                Angle = npc.PawnEquipment.WeaponRightSlot.Data.Angle;
            }
            else
            {
                Angle = npc.PawnEquipment.WeaponLeftSlot.Data.Angle;
            }
            return base.InAngleToUse(npc);
        }
    }
}