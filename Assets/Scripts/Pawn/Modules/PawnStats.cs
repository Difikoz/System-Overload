using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStats : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnEnergyChanged;
        public Action<List<Stat>> OnStatChanged;

        private PawnController _pawn;

        #region Stats
        [HideInInspector] public List<Stat> Stats = new();

        [HideInInspector] public float HealthCurrent;
        [HideInInspector] public float EnergyCurrent;

        [HideInInspector] public Stat HealthMax;
        [HideInInspector] public Stat EnergyMax;

        [HideInInspector] public Stat HealthRegeneration;
        [HideInInspector] public Stat EnergyRegeneration;

        [HideInInspector] public Stat DamageDealt;

        [HideInInspector] public Stat SlicingDamage;
        [HideInInspector] public Stat PiercingDamage;
        [HideInInspector] public Stat BluntDamage;

        [HideInInspector] public Stat FireDamage;
        [HideInInspector] public Stat WaterDamage;
        [HideInInspector] public Stat AirDamage;

        [HideInInspector] public Stat HolyDamage;
        [HideInInspector] public Stat DarknessDamage;
        [HideInInspector] public Stat ChemicalDamage;

        [HideInInspector] public Stat DamageTaken;

        [HideInInspector] public Stat SlicingResistance;
        [HideInInspector] public Stat PiercingResistance;
        [HideInInspector] public Stat BluntResistance;

        [HideInInspector] public Stat FireResistance;
        [HideInInspector] public Stat WaterResistance;
        [HideInInspector] public Stat AirResistance;

        [HideInInspector] public Stat HolyResistance;
        [HideInInspector] public Stat DarknessResistance;
        [HideInInspector] public Stat ChemicalResistance;

        [HideInInspector] public Stat MoveSpeed;
        #endregion

        [SerializeField] private float _regenerationTickDelay = 1f;
        [SerializeField] private float _healthRegenerationDelay = 10f;
        [SerializeField] private float _energyRegenerationDelay = 5f;

        private float _healthRegenerationTimer;
        private float _healthTickTimer;
        private float _energyRegenerationTimer;
        private float _energyTickTimer;

        public float HealthPercent => HealthCurrent / HealthMax.CurrentValue;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void ReduceCurrentHealth(float value, ElementConfig element, PawnController source = null)
        {
            if (_pawn.IsDead || value <= 0f)
            {
                return;
            }
            float resistance = GetStatByName(element.ResistanceStat.DisplayName).CurrentValue;
            if (resistance < 100f && !_pawn.IsInvulnerable)
            {
                if (resistance != 0f)
                {
                    value -= value * resistance / 100f;
                }
                value *= DamageTaken.CurrentValue / 100f;
                HealthCurrent = Mathf.Clamp(HealthCurrent - value, 0f, HealthMax.CurrentValue);
                OnHealthChanged?.Invoke(HealthCurrent, HealthMax.CurrentValue);
                _healthRegenerationTimer = 0f;
                if (HealthCurrent <= 0f)
                {
                    _pawn.Die(source);
                }
            }
            else if (resistance > 100f)
            {
                resistance -= 100f;// хил на разницу выше 100% сопротивления
                value *= resistance;
                RestoreCurrentHealth(value / 100f);
            }
        }

        public void RestoreCurrentHealth(float value)
        {
            if (_pawn.IsDead || value <= 0f)
            {
                return;
            }
            HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0f, HealthMax.CurrentValue);
            OnHealthChanged?.Invoke(HealthCurrent, HealthMax.CurrentValue);
        }

        public void ReduceCurrentEnergy(float value)
        {
            if (_pawn.IsDead || value <= 0f)
            {
                return;
            }
            EnergyCurrent = Mathf.Clamp(EnergyCurrent - value, 0f, EnergyMax.CurrentValue);
            OnEnergyChanged?.Invoke(EnergyCurrent, EnergyMax.CurrentValue);
            _energyRegenerationTimer = 0f;
        }

        public void RestoreCurrentEnergy(float value)
        {
            if (_pawn.IsDead || value <= 0f)
            {
                return;
            }
            EnergyCurrent = Mathf.Clamp(EnergyCurrent + value, 0f, EnergyMax.CurrentValue);
            OnEnergyChanged?.Invoke(EnergyCurrent, EnergyMax.CurrentValue);
        }

        public void CreateStats()
        {
            Stats = new(GameManager.StaticInstance.WorldData.GetStats());
            AssignStats();
        }

        public void AssignStats()
        {
            foreach (Stat s in Stats)
            {
                if (s.Data.DisplayName == "Health")
                {
                    HealthMax = s;
                }
                else if (s.Data.DisplayName == "Energy")
                {
                    EnergyMax = s;
                }
                else if (s.Data.DisplayName == "Health Regeneration")
                {
                    HealthRegeneration = s;
                }
                else if (s.Data.DisplayName == "Energy Regeneration")
                {
                    EnergyRegeneration = s;
                }
                else if (s.Data.DisplayName == "Damage Dealt")
                {
                    DamageDealt = s;
                }
                else if (s.Data.DisplayName == "Slicing Damage")
                {
                    SlicingDamage = s;
                }
                else if (s.Data.DisplayName == "Piercing Damage")
                {
                    PiercingDamage = s;
                }
                else if (s.Data.DisplayName == "Blunt Damage")
                {
                    BluntDamage = s;
                }
                else if (s.Data.DisplayName == "Fire Damage")
                {
                    FireDamage = s;
                }
                else if (s.Data.DisplayName == "Water Damage")
                {
                    WaterDamage = s;
                }
                else if (s.Data.DisplayName == "Air Damage")
                {
                    AirDamage = s;
                }
                else if (s.Data.DisplayName == "Holy Damage")
                {
                    HolyDamage = s;
                }
                else if (s.Data.DisplayName == "Darkness Damage")
                {
                    DarknessDamage = s;
                }
                else if (s.Data.DisplayName == "Chemical Damage")
                {
                    ChemicalDamage = s;
                }
                else if (s.Data.DisplayName == "Damage Taken")
                {
                    DamageTaken = s;
                }
                else if (s.Data.DisplayName == "Slicing Resistance")
                {
                    SlicingResistance = s;
                }
                else if (s.Data.DisplayName == "Piercing Resistance")
                {
                    PiercingResistance = s;
                }
                else if (s.Data.DisplayName == "Blunt Resistance")
                {
                    BluntResistance = s;
                }
                else if (s.Data.DisplayName == "Fire Resistance")
                {
                    FireResistance = s;
                }
                else if (s.Data.DisplayName == "Water Resistance")
                {
                    WaterResistance = s;
                }
                else if (s.Data.DisplayName == "Air Resistance")
                {
                    AirResistance = s;
                }
                else if (s.Data.DisplayName == "Holy Resistance")
                {
                    HolyResistance = s;
                }
                else if (s.Data.DisplayName == "Darkness Resistance")
                {
                    DarknessResistance = s;
                }
                else if (s.Data.DisplayName == "Chemical Resistance")
                {
                    ChemicalResistance = s;
                }
                else if (s.Data.DisplayName == "Move Speed")
                {
                    MoveSpeed = s;
                }
            }
        }

        public void AddStatModifier(StatModifierCreator creator)
        {
            GetStatByName(creator.Stat.DisplayName).AddModifier(creator.Modifier);
            OnStatChanged?.Invoke(Stats);
        }

        public void RemoveStatModifier(StatModifierCreator creator)
        {
            GetStatByName(creator.Stat.DisplayName).RemoveModifier(creator.Modifier);
            OnStatChanged?.Invoke(Stats);
        }

        public Stat GetStatByName(string name)
        {
            foreach (Stat s in Stats)
            {
                if (s.Data.DisplayName == name)
                {
                    return s;
                }
            }
            return null;
        }

        public void RecalculateStats()
        {
            foreach (Stat s in Stats)
            {
                s.CalculateCurrentValue();
            }
            HealthCurrent = Mathf.Clamp(HealthCurrent, 0f, HealthMax.CurrentValue);
            OnHealthChanged?.Invoke(HealthCurrent, HealthMax.CurrentValue);
            EnergyCurrent = Mathf.Clamp(EnergyCurrent, 0f, EnergyMax.CurrentValue);
            OnEnergyChanged?.Invoke(EnergyCurrent, EnergyMax.CurrentValue);
        }

        public void RegenerateHealth()
        {
            if (_healthRegenerationTimer >= _healthRegenerationDelay)
            {
                if (HealthCurrent < HealthMax.CurrentValue)
                {
                    _healthTickTimer += Time.deltaTime;
                    if (_healthTickTimer >= _regenerationTickDelay)
                    {
                        _healthTickTimer = 0f;
                        RestoreCurrentHealth(HealthRegeneration.CurrentValue);
                    }
                }
            }
            else
            {
                _healthRegenerationTimer += Time.deltaTime;
            }
        }

        public void RegenerateEnergy()
        {
            if (_pawn.IsRunning || _pawn.IsPerfomingAction)
            {
                return;
            }
            if (_energyRegenerationTimer >= _energyRegenerationDelay)
            {
                if (EnergyCurrent < EnergyMax.CurrentValue)
                {
                    _energyTickTimer += Time.deltaTime;
                    if (_energyTickTimer >= _regenerationTickDelay)
                    {
                        _energyTickTimer = 0f;
                        RestoreCurrentEnergy(EnergyRegeneration.CurrentValue);
                    }
                }
            }
            else
            {
                _energyRegenerationTimer += Time.deltaTime;
            }
        }
    }
}