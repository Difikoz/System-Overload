using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Combat Phase", menuName = "Winter Universe/Character/NPC/Phase/New Combat Phase")]
    public class CombatPhase : ScriptableObject
    {
        [HideInInspector] public int CurrentIndex;
        [HideInInspector] public PhaseTransition CurrentPhase;
        public List<PhaseTransition> Phases = new();

        public bool IsReadyToChangePhase(float healthPercent)
        {
            if (healthPercent < CurrentPhase.PercentToNext && CurrentIndex < Phases.Count - 1)
            {
                CurrentIndex++;
                CurrentPhase = Phases[CurrentIndex];
                return true;
            }
            else if (healthPercent > CurrentPhase.PercentToReset && CurrentIndex > 0)
            {
                CurrentIndex--;
                CurrentPhase = Phases[CurrentIndex];
                return true;
            }
            return false;
        }

        public void InstantiateStates()
        {
            foreach (PhaseTransition phaseTransition in Phases)
            {
                phaseTransition.State = Instantiate(phaseTransition.State);
            }
        }

        public void ResetPhase()
        {
            CurrentIndex = 0;
            CurrentPhase = Phases[CurrentIndex];
        }
    }

    [System.Serializable]
    public class PhaseTransition
    {
        public CombatState State;
        [Range(0.01f, 0.99f)] public float PercentToNext = 0.25f;
        [Range(0.01f, 0.99f)] public float PercentToReset = 0.75f;
    }
}