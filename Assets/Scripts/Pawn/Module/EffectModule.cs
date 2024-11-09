using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectModule : MonoBehaviour
    {
        private PawnController _owner;

        [SerializeField] private GameObject _bloodSplatterVFX;

        [HideInInspector] public List<Effect> Effects = new();

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        public void TickEffects(float deltaTime)
        {
            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                Effects[i].OnTick(deltaTime);
            }
        }

        public void AddEffect(Effect effect)
        {
            // change to spawning prefab
            Effects.Add(effect);
            effect.OnApply();
        }

        public void RemoveEffect(Effect effect)
        {
            if (Effects.Contains(effect))
            {
                effect.OnRemove();
                Effects.Remove(effect);
            }
        }

        public void RemovePositiveEffects()
        {
            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                if (Effects[i].Data.IsPositive)
                {
                    Effects[i].OnRemove();
                    Effects.RemoveAt(i);
                }
            }
        }

        public void RemoveNegativeEffects()
        {
            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                if (!Effects[i].Data.IsPositive)
                {
                    Effects[i].OnRemove();
                    Effects.RemoveAt(i);
                }
            }
        }

        public void SpawnBloodSplatterVFX(Vector3 position)
        {
            if (_bloodSplatterVFX != null)
            {
                LeanPool.Spawn(_bloodSplatterVFX, position, Quaternion.identity);
            }
        }

        public void SpawnBloodSplatterVFX(Vector3 position, Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                SpawnBloodSplatterVFX(position);
            }
            else if (_bloodSplatterVFX != null)
            {
                LeanPool.Spawn(_bloodSplatterVFX, position, Quaternion.LookRotation(direction));
            }
        }
    }
}