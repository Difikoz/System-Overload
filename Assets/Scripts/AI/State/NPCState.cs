using UnityEngine;

namespace WinterUniverse
{
    public class NPCState : ScriptableObject
    {
        public virtual NPCState Tick(AIController npc)
        {
            return this;
        }

        protected virtual NPCState SwitchState(AIController npc, NPCState state)
        {
            ResetFlags(npc);
            return state;
        }

        protected virtual void ResetFlags(AIController npc)
        {

        }
    }
}