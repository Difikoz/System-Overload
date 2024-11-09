using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldDataManager : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        public GameObject NPCPrefab;
        public GameObject LootItemPrefab;
        public GameObject LootBagPrefab;
        public float Gravity = -20f;
        public ExperienceConfig LevelConfig;
        public InstantHealthReduceEffectConfig HealthReduceEffect;
        public InstantHealthRestoreEffectConfig HealthRestoreEffect;
        public InstantEnergyReduceEffectConfig EnergyReduceEffect;
        public InstantEnergyRestoreEffectConfig EnergyRestoreEffect;
        public List<StatConfig> Stats = new();
        public List<RaceData> Races = new();
        public List<FactionData> Factions = new();
        public List<WeaponItemData> Weapons = new();
        public List<ArmorItemData> Armors = new();
        public List<ConsumableItemData> Consumables = new();
        public List<ResourceItemData> Resources = new();

        private List<Stat> _stats = new();
        private List<ItemData> _items = new();

        public void Initialize()
        {
            foreach (StatConfig data in Stats)
            {
                _stats.Add(new(data));
            }
            foreach (WeaponItemData data in Weapons)
            {
                _items.Add(data);
            }
            foreach (ArmorItemData data in Armors)
            {
                _items.Add(data);
            }
            foreach (ConsumableItemData data in Consumables)
            {
                _items.Add(data);
            }
            foreach (ResourceItemData data in Resources)
            {
                _items.Add(data);
            }
        }

        public List<Stat> GetStats()
        {
            return _stats;
        }

        public StatConfig GetStat(string name)
        {
            foreach (StatConfig data in Stats)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public RaceData GetRace(string name)
        {
            foreach (RaceData data in Races)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public FactionData GetFaction(string name)
        {
            foreach (FactionData data in Factions)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ItemData GetItem(string name)
        {
            foreach (ItemData data in _items)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public WeaponItemData GetWeapon(string name)
        {
            foreach (WeaponItemData data in Weapons)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ArmorItemData GetArmor(string name)
        {
            foreach (ArmorItemData data in Armors)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ConsumableItemData GetConsumable(string name)
        {
            foreach (ConsumableItemData data in Consumables)
            {
                if (data.DisplayName == name)
                {
                    return data;
                }
            }
            return null;
        }

        public ResourceItemData GetResource(string name)
        {
            foreach (ResourceItemData data in Resources)
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