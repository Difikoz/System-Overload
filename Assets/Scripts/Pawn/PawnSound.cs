using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(AudioSource))]
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;
        private AudioSource _audioSource;

        [SerializeField] private List<AudioClip> _maleAttackClips = new();
        [SerializeField] private List<AudioClip> _maleGetHitClips = new();
        [SerializeField] private List<AudioClip> _maleDeathClips = new();

        [SerializeField] private List<AudioClip> _femaleAttackClips = new();
        [SerializeField] private List<AudioClip> _femaleGetHitClips = new();
        [SerializeField] private List<AudioClip> _femaleDeathClips = new();

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAttackClip()
        {
            if (_pawn.Gender == Gender.Male && _maleAttackClips.Count > 0)
            {
                PlaySound(_maleAttackClips);
            }
            else if (_pawn.Gender == Gender.Female && _femaleAttackClips.Count > 0)
            {
                PlaySound(_femaleAttackClips);
            }
        }

        public void PlayGetHitClip()
        {
            if (_pawn.Gender == Gender.Male && _maleGetHitClips.Count > 0)
            {
                PlaySound(_maleGetHitClips);
            }
            else if (_pawn.Gender == Gender.Female && _femaleGetHitClips.Count > 0)
            {
                PlaySound(_femaleGetHitClips);
            }
        }

        public void PlayDeathClip()
        {
            if (_pawn.Gender == Gender.Male && _maleDeathClips.Count > 0)
            {
                PlaySound(_maleDeathClips);
            }
            else if (_pawn.Gender == Gender.Female && _femaleDeathClips.Count > 0)
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
            _audioSource.volume = volume;
            _audioSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;
            _audioSource.PlayOneShot(clip);
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