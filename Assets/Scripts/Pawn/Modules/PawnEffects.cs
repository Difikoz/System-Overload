using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEffects : MonoBehaviour
    {
        private PawnController _pawn;
        private List<Effect> _effects = new();

        [SerializeField] private GameObject _bloodSplatterVFX;

        public List<Effect> Effects => _effects;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void TickEffects(float deltaTime)
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                _effects[i].OnTick(deltaTime);
            }
        }

        public void AddEffect(Effect effect)
        {
            // change to spawning prefab
            _effects.Add(effect);
            effect.OnApply();
        }

        public void RemoveEffect(Effect effect)
        {
            if (_effects.Contains(effect))
            {
                effect.OnRemove();
                _effects.Remove(effect);
            }
        }

        public void RemovePositiveEffects()
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                if (_effects[i].Config.IsPositive)
                {
                    _effects[i].OnRemove();
                    _effects.RemoveAt(i);
                }
            }
        }

        public void RemoveNegativeEffects()
        {
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                if (!_effects[i].Config.IsPositive)
                {
                    _effects[i].OnRemove();
                    _effects.RemoveAt(i);
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