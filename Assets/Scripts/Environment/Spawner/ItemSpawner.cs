using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class ItemSpawner : MonoBehaviour
    {
        public ItemData Item;
        public int Amount = 1;

        private ItemInteractable _spawnedItem;

        private void OnEnable()
        {
            Spawn();
        }

        public void Spawn()
        {
            if (_spawnedItem != null)
            {
                LeanPool.Despawn(_spawnedItem.gameObject);
            }
            _spawnedItem = LeanPool.Spawn(WorldDataManager.StaticInstance.LootItemPrefab, transform.position, transform.rotation).GetComponent<ItemInteractable>();
            _spawnedItem.Setup(Item, Amount);
        }
    }
}