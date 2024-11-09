using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerStatUI : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private GameObject _slotPrefab;

        public void ShowUI()
        {

        }

        public void HideUI()
        {

        }

        public void UpdateUI(List<Stat> stats)
        {
            while (_root.childCount > 0)
            {
                Destroy(_root.GetChild(0).gameObject);// TODO pool despawn
            }
            foreach (Stat stat in stats)
            {
                Instantiate(_slotPrefab, _root).GetComponent<StatSlotUI>().Setup(stat);
            }
        }
    }
}