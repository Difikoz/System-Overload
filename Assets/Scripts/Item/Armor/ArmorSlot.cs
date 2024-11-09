using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        private PawnController _owner;
        private ArmorRenderer _currentRenderer;

        [HideInInspector] public ArmorItemData Data;

        public ArmorTypeData Type;
        [SerializeField] private ArmorRenderer _defaultRenderer;
        [SerializeField] private List<ArmorRenderer> _renderers = new();

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        public void Equip(ArmorItemData armor)
        {
            if (armor == null)
            {
                return;
            }
            DisableMeshes(_currentRenderer);
            Data = armor;// TODO need instantiate?
            foreach (StatModifierCreator creator in Data.Modifiers)
            {
                _owner.StatModule.AddStatModifier(creator);
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
                _owner.StatModule.RemoveStatModifier(creator);
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
            foreach (GameObject go in ar.NeutralParts)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in ar.MaleParts)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in ar.FemaleParts)
            {
                go.SetActive(false);
            }
        }

        private void EnableMeshes(ArmorRenderer ar)
        {
            foreach (GameObject go in ar.NeutralParts)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in ar.MaleParts)
            {
                go.SetActive(_owner.Gender == Gender.Male);
            }
            foreach (GameObject go in ar.FemaleParts)
            {
                go.SetActive(_owner.Gender == Gender.Female);
            }
        }
    }

    [System.Serializable]
    public class ArmorRenderer
    {
        public ArmorItemData Data;
        public List<GameObject> NeutralParts;
        public List<GameObject> MaleParts;
        public List<GameObject> FemaleParts;
    }
}