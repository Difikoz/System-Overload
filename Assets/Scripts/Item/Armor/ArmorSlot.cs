using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private ArmorRenderer _currentRenderer;

        [SerializeField] private ArmorItemConfig _data;
        [SerializeField] private ArmorTypeConfig _type;
        [SerializeField] private List<ArmorRenderer> _renderers = new();

        public ArmorItemConfig Data => _data;
        public ArmorTypeConfig Type => _type;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            foreach (StatModifierCreator creator in _data.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            ForceUpdateMeshes();
        }

        public void Equip(ArmorItemConfig armor)
        {
            if (armor == null)
            {
                return;
            }
            foreach (StatModifierCreator creator in _data.Modifiers)
            {
                _pawn.PawnStats.RemoveStatModifier(creator);
            }
            DisableMeshes(_currentRenderer);
            _data = armor;
            foreach (StatModifierCreator creator in _data.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            foreach (ArmorRenderer ar in _renderers)
            {
                if (ar.Data == _data)
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
                if (ar.Data == _data)
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