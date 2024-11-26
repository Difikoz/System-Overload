using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldDataManager : MonoBehaviour
    {
        private List<Stat> _newStats = new();
        private List<ItemConfig> _items = new();

        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _aiPrefab;
        [SerializeField] private GameObject _lootItemPrefab;
        [SerializeField] private GameObject _lootBagPrefab;
        [SerializeField] private float _gravity = -20f;
        [SerializeField] private InstantHealthReduceEffectConfig _healthReduceEffect;
        [SerializeField] private InstantHealthRestoreEffectConfig _healthRestoreEffect;
        [SerializeField] private InstantEnergyReduceEffectConfig _energyReduceEffect;
        [SerializeField] private InstantEnergyRestoreEffectConfig _energyRestoreEffect;
        [SerializeField] private List<StatConfig> _stats = new();
        [SerializeField] private List<FactionConfig> _factions = new();
        [SerializeField] private List<WeaponItemConfig> _weapons = new();
        [SerializeField] private List<ArmorItemConfig> _armors = new();
        [SerializeField] private List<ConsumableItemConfig> _consumables = new();
        [SerializeField] private List<ResourceItemConfig> _resources = new();

        public GameObject PawnPrefab => _pawnPrefab;
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject AIPrefab => _aiPrefab;
        public GameObject LootItemPrefab => _lootItemPrefab;
        public GameObject LootBagPrefab => _lootBagPrefab;
        public float Gravity => _gravity;
        public InstantHealthReduceEffectConfig HealthReduceEffect => _healthReduceEffect;
        public InstantHealthRestoreEffectConfig HealthRestoreEffect => _healthRestoreEffect;
        public InstantEnergyReduceEffectConfig EnergyReduceEffect => _energyReduceEffect;
        public InstantEnergyRestoreEffectConfig EnergyRestoreEffect => _energyRestoreEffect;
        public List<StatConfig> Stats => _stats;
        public List<FactionConfig> Factions => _factions;
        public List<ItemConfig> Items => _items;
        public List<WeaponItemConfig> Weapons => _weapons;
        public List<ArmorItemConfig> Armors => _armors;
        public List<ConsumableItemConfig> Consumables => _consumables;
        public List<ResourceItemConfig> Resources => _resources;

        public void Initialize()
        {
            foreach (StatConfig data in _stats)
            {
                _newStats.Add(new(data));
            }
            foreach (WeaponItemConfig data in _weapons)
            {
                _items.Add(data);
            }
            foreach (ArmorItemConfig data in _armors)
            {
                _items.Add(data);
            }
            foreach (ConsumableItemConfig data in _consumables)
            {
                _items.Add(data);
            }
            foreach (ResourceItemConfig data in _resources)
            {
                _items.Add(data);
            }
        }

        public List<Stat> GetStats()
        {
            return _newStats;
        }

        public StatConfig GetStat(string name)
        {
            foreach (StatConfig data in _stats)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public FactionConfig GetFaction(string name)
        {
            foreach (FactionConfig data in _factions)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ItemConfig GetItem(string name)
        {
            foreach (ItemConfig data in _items)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public WeaponItemConfig GetWeapon(string name)
        {
            foreach (WeaponItemConfig data in _weapons)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ArmorItemConfig GetArmor(string name)
        {
            foreach (ArmorItemConfig data in _armors)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ConsumableItemConfig GetConsumable(string name)
        {
            foreach (ConsumableItemConfig data in _consumables)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ResourceItemConfig GetResource(string name)
        {
            foreach (ResourceItemConfig data in _resources)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }
    }
}