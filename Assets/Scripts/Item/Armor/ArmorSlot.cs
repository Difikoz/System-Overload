using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        private PawnController _owner;
        private ArmorRenderer _currentRenderer;

        [HideInInspector] public ArmorItemConfig Data;

        public ArmorTypeData Type;
        [SerializeField] private ArmorRenderer _defaultRenderer;
        [SerializeField] private List<ArmorRenderer> _renderers = new();

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        public void Equip(ArmorItemConfig armor)
        {
            if (armor == null)
            {
                return;
            }
            DisableMeshes(_currentRenderer);
            Data = armor;// TODO need instantiate?
            foreach (StatModifierCreator creator in Data.Modifiers)
            {
                _owner.PawnStats.AddStatModifier(creator);
            }
            foreach (ArmorRenderer ar in _renderers)
            {
                if (ar.Data == Data)
                {
                    _currentRenderer = ar;
                    break;
                }
            }
            EnableMeshes(_currentRenderer);
        }

        public void Unequip()
        {
            if (Data == null)
            {
                return;
            }
            foreach (StatModifierCreator creator in Data.Modifiers)
            {
                _owner.PawnStats.RemoveStatModifier(creator);
            }
            Data = null;
            DisableMeshes(_currentRenderer);
            _currentRenderer = _defaultRenderer;
            EnableMeshes(_currentRenderer);
        }

        public void ForceUpdateMeshes()
        {
            foreach (ArmorRenderer ar in _renderers)
            {
                DisableMeshes(ar);
            }
            DisableMeshes(_defaultRenderer);
            if (Data != null)
            {
                foreach (ArmorRenderer ar in _renderers)
                {
                    if (ar.Data == Data)
                    {
                        _currentRenderer = ar;
                        break;
                    }
                }
            }
            else
            {
                _currentRenderer = _defaultRenderer;
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