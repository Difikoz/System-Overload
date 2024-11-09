using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Resource Item", menuName = "Winter Universe/Item/Resource/New Item")]
    public class ResourceItemConfig : ItemConfig
    {
        private void OnValidate()
        {
            _itemType = ItemType.Resource;
        }
    }
}