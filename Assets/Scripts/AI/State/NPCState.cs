using UnityEngine;

namespace WinterUniverse
{
    public class NPCState : ScriptableObject
    {
        public virtual NPCState Tick(NPCController npc)
        {
            return this;
        }

        protected virtual NPCState SwitchState(NPCController npc, NPCState state)
        {
            ResetFlags(npc);
            return state;
        }

        protected virtual void ResetFlags(NPCController npc)
        {

        }
    }
}