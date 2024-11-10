using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EquipmentUI : MonoBehaviour
    {
        //[SerializeField] private Transform _statRoot;
        //[SerializeField] private GameObject _statSlotPrefab;

        public void Initialize()
        {
            //GameManager.StaticInstance.Player.PawnStats.OnStatChanged += UpdateUI;
        }

        //private void UpdateUI(List<Stat> stats)
        //{
        //    while (_statRoot.childCount > 0)
        //    {
        //        LeanPool.Despawn(_statRoot.GetChild(0).gameObject);
        //    }
        //    foreach (Stat stat in stats)
        //    {
        //        LeanPool.Spawn(_statSlotPrefab, _statRoot).GetComponent<StatSlotUI>().Setup(stat);
        //    }
        //}
    }
}