using UnityEngine;

namespace WinterUniverse
{
    public class AbilityCastTypeData : ScriptableObject
    {
        public float EnergyCost = 10f;
        public bool PlayStartAnimation = true;
        public string StartAnimationName = "Cast Projectile Start";
        public bool PlayCompleteAnimation = true;
        public string CompleteAnimationName = "Cast Projectile Complete";

        public virtual bool CanCast(Character caster, Character target)
        {
            if (caster.IsPerfomingAction && !caster.IsCasting || caster.StatModule.EnergyCurrent < EnergyCost)
            {
                return false;
            }
            return true;
        }

        public virtual void OnCastStart(Character caster, Character target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            if (PlayStartAnimation)
            {
                caster.AnimatorModule.PlayActionAnimation(StartAnimationName, true);
            }
        }

        public virtual void OnCastComplete(Character caster, Character target, Vector3 position, Vector3 direction, AbilityHitTypeData effect)
        {
            if (PlayCompleteAnimation)
            {
                caster.AnimatorModule.PlayActionAnimation(CompleteAnimationName, true);
            }
            caster.StatModule.ReduceCurrentEnergy(EnergyCost);
        }
    }
}