using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class ArmorRenderer
    {
        [SerializeField] private ArmorItemConfig _data;
        [SerializeField] private List<GameObject> _meshes = new();

        public ArmorItemConfig Data => _data;
        public List<GameObject> Meshes => _meshes;
    }
}