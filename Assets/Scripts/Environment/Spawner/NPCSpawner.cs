using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCSpawner : MonoBehaviour
    {
        public CharacterData Data;

        private NPCController _spawnedNPC;

        private void OnEnable()
        {
            Spawn();
        }

        public void Spawn()
        {
            if (_spawnedNPC == null)
            {
                _spawnedNPC = LeanPool.Spawn(GameManager.StaticInstance.WorldData.NPCPrefab, transform.position, transform.rotation).GetComponent<NPCController>();
                _spawnedNPC.CreateCharacter(Data.GetData());
            }
            else if (_spawnedNPC.IsDead)
            {
                _spawnedNPC.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                _spawnedNPC.Revive();
            }
        }
    }
}