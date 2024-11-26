using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class ArmorRenderer
    {
        [SerializeField] private ArmorItemConfig _config;
        [SerializeField] private List<GameObject> _meshes = new();

        public ArmorItemConfig Config => _config;
        public List<GameObject> Meshes => _meshes;
    }
}