using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Primary Attack Action", menuName = "Winter Universe/Character/NPC/Action/New Primary Attack")]
    public class NPCPrimaryAttackActionData : NPCActionData
    {
        public HandSlotType Slot;

        public override void AttempToPerformAction(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.EquipmentModule.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                npc.CombatModule.UseWeaponAbility(npc.EquipmentModule.WeaponRightSlot.Data, HandSlotType.Right, npc.EquipmentModule.WeaponRightSlot.Data.PrimaryAbility);
            }
            else
            {
                npc.CombatModule.UseWeaponAbility(npc.EquipmentModule.WeaponLeftSlot.Data, HandSlotType.Left, npc.EquipmentModule.WeaponLeftSlot.Data.PrimaryAbility);
            }
        }

        public override bool InRangeToUse(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.EquipmentModule.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                MinDistance = npc.EquipmentModule.WeaponRightSlot.Data.MinDistance;
                MaxDistance = npc.EquipmentModule.WeaponRightSlot.Data.MaxDistance;
            }
            else
            {
                MinDistance = npc.EquipmentModule.WeaponLeftSlot.Data.MinDistance;
                MaxDistance = npc.EquipmentModule.WeaponLeftSlot.Data.MaxDistance;
            }
            return base.InRangeToUse(npc);
        }

        public override bool InAngleToUse(NPCController npc)
        {
            if (Slot == HandSlotType.Right || npc.EquipmentModule.WeaponLeftSlot.Data.DisplayName == "Unarmed")
            {
                Angle = npc.EquipmentModule.WeaponRightSlot.Data.Angle;
            }
            else
            {
                Angle = npc.EquipmentModule.WeaponLeftSlot.Data.Angle;
            }
            return base.InAngleToUse(npc);
        }
    }
}