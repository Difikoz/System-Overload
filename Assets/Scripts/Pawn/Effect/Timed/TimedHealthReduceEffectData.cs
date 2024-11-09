using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Timed Health Reduce", menuName = "Winter Universe/Effect/Timed/New Health Reduce")]
    public class TimedHealthReduceEffectData : EffectData
    {
        public ElementData Element;

        public bool PlayDamageAnimation;
        public bool PlayDamageVFX;
        public bool PlayDamageSFX;

        public override Effect CreateEffect()
        {
            return new TimedHealthReduceEffect(this);
        }
    }

    public class TimedHealthReduceEffect : Effect
    {
        public ElementData Element;

        public bool PlayDamageAnimation;
        public bool PlayDamageVFX;
        public bool PlayDamageSFX;

        public TimedHealthReduceEffect(TimedHealthReduceEffectData data) : base(data)
        {
            if (Element == null)
            {
                Element = data.Element;
                PlayDamageAnimation = data.PlayDamageAnimation;
                PlayDamageVFX = data.PlayDamageVFX;
                PlayDamageSFX = data.PlayDamageSFX;
            }
        }

        public override void OnApply()
        {
            //if (PlayDamageVFX)
            //{
            //    if (Element.HitVFX.Count > 0)
            //    {
            //        Object.Instantiate(Element.HitVFX[Random.Range(0, Element.HitVFX.Count)], Owner.CharacterCombatManager.BodyPoint.position, Quaternion.identity);
            //    }
            //}
            //if (PlayDamageSFX)
            //{
            //    if (Element.HitClips.Count > 0)
            //    {
            //        Owner.CharacterSoundManager.PlaySound(WorldSoundManager.StaticInstance.ChooseRandomClip(Element.HitClips));
            //    }
            //    Owner.CharacterSoundManager.PlayGetHitClip();
            //}
            //if (PlayDamageAnimation)
            //{
            //    if (ManualAnimationName == string.Empty)
            //    {
            //        Owner.CharacterAnimatorManager.PlayActionAnimation("Get Hit Forward", true);
            //    }
            //    else
            //    {
            //        Owner.CharacterAnimatorManager.PlayActionAnimation(ManualAnimationName, true);
            //    }
            //}
        }

        public override void OnTick(float deltaTime)
        {
            if (Duration > 0f)
            {
                ProcessEffect(deltaTime);
                Duration -= deltaTime;
            }
            else
            {
                Owner.EffectModule.RemoveEffect(this);
            }
        }

        private void ProcessEffect(float deltaTime)
        {
            if (Owner.IsDead)
            {
                return;
            }
            Owner.StatModule.ReduceCurrentHealth(Value * deltaTime, Element, Source);
        }
    }
}