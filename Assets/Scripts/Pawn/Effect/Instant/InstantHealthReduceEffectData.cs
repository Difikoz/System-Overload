using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Instant Health Reduce", menuName = "Winter Universe/Effect/Instant/New Health Reduce")]
    public class InstantHealthReduceEffectData : EffectData
    {
        public override Effect CreateEffect()
        {
            return new InstantHealthReduceEffect(this);
        }
    }

    public class InstantHealthReduceEffect : Effect
    {
        public ElementData Element;

        public bool PlayDamageAnimation = true;
        public bool PlayDamageVFX = true;
        public bool PlayDamageSFX = true;

        public Vector3 HitPoint;
        public Vector3 HitDirection;
        public float AngleHitFrom;

        public InstantHealthReduceEffect(InstantHealthReduceEffectData data) : base(data)
        {
        }

        public override void OnApply()
        {
            ProcessEffect();
            Owner.PawnEffects.RemoveEffect(this);
        }

        private void ProcessEffect()
        {
            if (Owner.IsDead)
            {
                return;
            }
            if (PlayDamageSFX)
            {
                if (Element.HitClips.Count > 0)
                {
                    Owner.PawnSound.PlaySound(GameManager.StaticInstance.WorldSound.ChooseRandomClip(Element.HitClips));
                }
                Owner.PawnSound.PlayGetHitClip();
            }
            if (PlayDamageVFX)
            {
                if (Element.HitVFX.Count > 0)
                {
                    LeanPool.Spawn(Element.HitVFX[Random.Range(0, Element.HitVFX.Count)], HitPoint, HitDirection != Vector3.zero ? Quaternion.LookRotation(HitDirection) : Quaternion.identity);
                }
                Owner.PawnEffects.SpawnBloodSplatterVFX(HitPoint, HitDirection);
            }
            if (PlayDamageAnimation)
            {
                PlayDirectionBasedDamageAnimation();
            }
            Owner.PawnStats.ReduceCurrentHealth(Value < 1f ? 1f : Value, Element, Source);
        }

        private void PlayDirectionBasedDamageAnimation()
        {
            if ((AngleHitFrom >= 135f && AngleHitFrom <= 180f) || (AngleHitFrom <= -135f && AngleHitFrom >= -180f))//front
            {
                Owner.PawnAnimator.PlayActionAnimation("Get Hit Forward", true);
            }
            else if (AngleHitFrom >= -45f && AngleHitFrom <= 45f)//back
            {
                Owner.PawnAnimator.PlayActionAnimation("Get Hit Backward", true);
            }
            else if (AngleHitFrom >= 45f && AngleHitFrom <= 135f)//right
            {
                Owner.PawnAnimator.PlayActionAnimation("Get Hit Right", true);
            }
            else if (AngleHitFrom >= -135f && AngleHitFrom <= -45f)//left
            {
                Owner.PawnAnimator.PlayActionAnimation("Get Hit Left", true);
            }
            else
            {
                Owner.PawnAnimator.PlayActionAnimation("Get Hit Forward", true);
            }
        }
    }
}