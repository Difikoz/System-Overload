using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Resource Item", menuName = "Winter Universe/Item/Resource/New Item")]
    public class ResourceItemConfig : ItemConfig
    {
        public override bool Use(PawnController character, bool fromInventory = true)
        {
            return false;
        }
    }
}