using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCSpawner : MonoBehaviour
    {
        public PawnConfig Data;

        private AIController _spawnedNPC;

        private void OnEnable()
        {
            Spawn();
        }

        public void Spawn()
        {
            if (_spawnedNPC == null)
            {
                _spawnedNPC = LeanPool.Spawn(GameManager.StaticInstance.WorldData.AIPrefab, transform.position, transform.rotation).GetComponent<AIController>();
                _spawnedNPC.Initialize();
                _spawnedNPC.Pawn.CreateCharacter(Data.GetData());
            }
            else if (_spawnedNPC.Pawn.IsDead)
            {
                _spawnedNPC.Pawn.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                _spawnedNPC.Pawn.Revive();
            }
        }
    }
}