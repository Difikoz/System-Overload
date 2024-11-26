using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private WeaponItemConfig _config;

        private PawnController _pawn;
        private DamageCollider _damageCollider;
        private GameObject _model;

        public PawnController Pawn => _pawn;
        public WeaponItemConfig Config => _config;
        public DamageCollider DamageCollider => _damageCollider;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
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