using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundModule : MonoBehaviour
    {
        private PawnController _owner;

        [HideInInspector] public AudioSource AudioSource;

        [SerializeField] private List<AudioClip> _maleAttackClips = new();
        [SerializeField] private List<AudioClip> _maleGetHitClips = new();
        [SerializeField] private List<AudioClip> _maleDeathClips = new();

        [SerializeField] private List<AudioClip> _femaleAttackClips = new();
        [SerializeField] private List<AudioClip> _femaleGetHitClips = new();
        [SerializeField] private List<AudioClip> _femaleDeathClips = new();

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _owner = GetComponentInParent<PawnController>();
        }

        public void PlayAttackClip()
        {
            if (_owner.Gender == Gender.Male && _maleAttackClips.Count > 0)
            {
                PlaySound(_maleAttackClips);
            }
            else if (_owner.Gender == Gender.Female && _femaleAttackClips.Count > 0)
            {
                PlaySound(_femaleAttackClips);
            }
        }

        public void PlayGetHitClip()
        {
            if (_owner.Gender == Gender.Male && _maleGetHitClips.Count > 0)
            {
                PlaySound(_maleGetHitClips);
            }
            else if (_owner.Gender == Gender.Female && _femaleGetHitClips.Count > 0)
            {
                PlaySound(_femaleGetHitClips);
            }
        }

        public void PlayDeathClip()
        {
            if (_owner.Gender == Gender.Male && _maleDeathClips.Count > 0)
            {
                PlaySound(_maleDeathClips);
            }
            else if (_owner.Gender == Gender.Female && _femaleDeathClips.Count > 0)
            {
                PlaySound(_femaleDeathClips);
            }
        }

        public void PlaySound(AudioClip clip, bool randomizePitch = true, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (clip == null)
            {
                return;
            }
            AudioSource.volume = volume;
            AudioSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;
            AudioSource.PlayOneShot(clip);
        }

        public void PlaySound(List<AudioClip> clips, bool randomizePitch = true, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (clips.Count == 0)
            {
                return;
            }
            PlaySound(clips[Random.Range(0, clips.Count)], randomizePitch, volume, minPitch, maxPitch);
        }
    }
}