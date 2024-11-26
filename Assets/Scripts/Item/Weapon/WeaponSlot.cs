using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private DamageCollider _damageCollider;
        private GameObject _model;

        public PawnController Pawn => _pawn;
        public WeaponItemConfig Config => _config;
        public DamageCollider DamageCollider => _damageCollider;

        public void Initialize(WeaponItemConfig defaultWeapon)
        {
            _pawn = GetComponentInParent<PawnController>();
            _config = defaultWeapon;
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            _model = LeanPool.Spawn(_config.Model, transform);
            _model.transform.SetLocalPositionAndRotation(_config.LocalPosition, _config.LocalRotation);
            _damageCollider = _model.GetComponentInChildren<DamageCollider>();
            _damageCollider.Initialize(_pawn, _config.DamageTypes, _config.OwnerEffects, _config.TargetEffects, _config.DoSplashDamage, _config.SplashRadius);
        }

        public void Setup(WeaponItemConfig weapon)
        {
            if (weapon == null)
            {
                return;
            }
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.RemoveStatModifier(creator);
            }
            LeanPool.Despawn(_model);
            _config = weapon;
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            _model = LeanPool.Spawn(_config.Model, transform);
            _model.transform.SetLocalPositionAndRotation(_config.LocalPosition, _config.LocalRotation);
            _damageCollider = _model.GetComponentInChildren<DamageCollider>();
            _damageCollider.Initialize(_pawn, _config.DamageTypes, _config.OwnerEffects, _config.TargetEffects, _config.DoSplashDamage, _config.SplashRadius);
        }
    }
}