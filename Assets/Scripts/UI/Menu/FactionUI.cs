using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class FactionUI : MonoBehaviour
    {
        [SerializeField] private Transform _factionRoot;
        [SerializeField] private GameObject _factionSlotPrefab;

        public void Initialize()
        {
            GameManager.StaticInstance.Player.Pawn.OnFactionChanged += UpdateUI;
        }

        private void UpdateUI(FactionConfig faction)
        {
            while (_factionRoot.childCount > 0)
            {
                LeanPool.Despawn(_factionRoot.GetChild(0).gameObject);
            }
            foreach (FactionRelationship fr in faction.Relationships)
            {
                LeanPool.Spawn(_factionSlotPrefab, _factionRoot).GetComponent<FactionSlotUI>().Setup(fr);
            }
        }
    }
}