using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private ArmorRenderer _currentRenderer;

        private ArmorItemConfig _config;
        [SerializeField] private List<ArmorRenderer> _renderers = new();

        public ArmorItemConfig Config => _config;

        public void Initialize(ArmorItemConfig defaultArmor)
        {
            _pawn = GetComponentInParent<PawnController>();
            _config = defaultArmor;
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            ForceUpdateMeshes();
        }

        public void Setup(ArmorItemConfig armor)
        {
            if (armor == null)
            {
                return;
            }
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.RemoveStatModifier(creator);
            }
            DisableMeshes(_currentRenderer);
            _config = armor;
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            foreach (ArmorRenderer ar in _renderers)
            {
                if (ar.Config == _config)
                {
                    _currentRenderer = ar;
                    break;
                }
            }
            EnableMeshes(_currentRenderer);
        }

        public void ForceUpdateMeshes()
        {
            foreach (ArmorRenderer ar in _renderers)
            {
                DisableMeshes(ar);
            }
            foreach (ArmorRenderer ar in _renderers)
            {
                if (ar.Config == _config)
                {
                    _currentRenderer = ar;
                    break;
                }
            }
            EnableMeshes(_currentRenderer);
        }

        private void DisableMeshes(ArmorRenderer ar)
        {
            foreach (GameObject go in ar.Meshes)
            {
                go.SetActive(false);
            }
        }

        private void EnableMeshes(ArmorRenderer ar)
        {
            foreach (GameObject go in ar.Meshes)
            {
                go.SetActive(true);
            }
        }
    }
}