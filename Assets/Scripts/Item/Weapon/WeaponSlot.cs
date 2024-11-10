using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private HandSlotType _type;

        private PawnController _pawn;
        private WeaponItemConfig _data;
        private DamageCollider _damageCollider;
        private GameObject _model;

        public PawnController Pawn => _pawn;
        public WeaponItemConfig Data => _data;
        public HandSlotType Type => _type;
        public DamageCollider DamageCollider => _damageCollider;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
        }

        public void Equip(WeaponItemConfig weapon)
        {
            if (weapon == null)
            {
                return;
            }
            _data = weapon;// TODO add instantiate?
            foreach (StatModifierCreator creator in _data.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            _model = LeanPool.Spawn(_data.Model, transform);
            _model.transform.SetLocalPositionAndRotation(_data.LocalPosition, _data.LocalRotation);
            _damageCollider = _model.GetComponentInChildren<DamageCollider>();
            _damageCollider.Initialize(_pawn, _data.DamageTypes, _data.OwnerEffects, _data.TargetEffects, _data.DoSplashDamage, _data.SplashRadius);
        }

        public void Unequip()
        {
            if (_data == null)
            {
                return;
            }
            foreach (StatModifierCreator creator in _data.Modifiers)
            {
                Pawn.PawnStats.RemoveStatModifier(creator);
            }
            _data = null;
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
        }
    }
}