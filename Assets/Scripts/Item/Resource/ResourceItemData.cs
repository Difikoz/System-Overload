using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Resource Item", menuName = "Winter Universe/Item/Resource/New Item")]
    public class ResourceItemData : ItemData
    {
        private void OnValidate()
        {
            ItemType = ItemType.Resource;
        }
    }
}