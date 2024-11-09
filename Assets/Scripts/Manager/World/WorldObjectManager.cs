using UnityEngine;

namespace WinterUniverse
{
    public class WorldObjectManager : Singleton<WorldObjectManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

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