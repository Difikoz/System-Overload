using UnityEngine;

namespace WinterUniverse
{
    public class WorldObjectManager : MonoBehaviour
    {
        [SerializeField] private GameObject _humanPrefab;
        [SerializeField] private GameObject _aiControllerPrefab;
        [SerializeField] private GameObject _lootItemPrefab;
        [SerializeField] private GameObject _lootBagPrefab;

        public GameObject HumanPrefab => _humanPrefab;
        public GameObject AIControllerPrefab => _aiControllerPrefab;
        public GameObject LootItemPrefab => _lootItemPrefab;
        public GameObject LootBagPrefab => _lootBagPrefab;
        //public RespawnPoint GetRespawnPointByID(int id)
        //{
        //    RespawnPoint[] respawnPoints = FindObjectsOfType<RespawnPoint>();
        //    foreach (RespawnPoint point in respawnPoints)
        //    {
        //        if (point.ID == id)
        //        {
        //            return point;
        //        }
        //    }
        //    return null;
        //}
    }
}