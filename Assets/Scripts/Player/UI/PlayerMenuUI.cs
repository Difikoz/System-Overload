using UnityEngine;

namespace WinterUniverse
{
    public class PlayerMenuUI : MonoBehaviour
    {
        [HideInInspector] public PlayerStatUI StatUI;
        [HideInInspector] public PlayerEquipmentUI EquipmentUI;
        [HideInInspector] public PlayerInventoryUI InventoryUI;
        [HideInInspector] public PlayerFactionUI FactionUI;
        [SerializeField] private GameObject _root;

        private void Awake()
        {
            StatUI = GetComponentInChildren<PlayerStatUI>();
            EquipmentUI = GetComponentInChildren<PlayerEquipmentUI>();
            InventoryUI = GetComponentInChildren<PlayerInventoryUI>();
            FactionUI = GetComponentInChildren<PlayerFactionUI>();
        }

        public void ShowMenu()
        {
            _root.SetActive(true);
        }

        public void HideMenu()
        {
            _root.SetActive(false);
        }
    }
}