using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryRoot;
        [SerializeField] private GameObject _inventorySlotPrefab;

        public void Initialize()
        {
            GameManager.StaticInstance.Player.PawnInventory.OnInventoryChanged += UpdateUI;
            gameObject.SetActive(false);
        }

        private void UpdateUI(List<ItemStack> stacks)
        {
            while (_inventoryRoot.childCount > 0)
            {
                LeanPool.Despawn(_inventoryRoot.GetChild(0).gameObject);
            }
            foreach (ItemStack stack in stacks)
            {
                LeanPool.Spawn(_inventorySlotPrefab, _inventoryRoot).GetComponent<InventorySlotUI>().Setup(stack);
            }
        }
    }
}