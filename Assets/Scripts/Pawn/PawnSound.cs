using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(AudioSource))]
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;
        private AudioSource _audioSource;

        [SerializeField] private List<AudioClip> _attackClips = new();
        [SerializeField] private List<AudioClip> _getHitClips = new();
        [SerializeField] private List<AudioClip> _deathClips = new();

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAttackClip()
        {
            if (_attackClips.Count > 0)
            {
                PlaySound(_attackClips);
            }
        }

        public void PlayGetHitClip()
        {
            if (_getHitClips.Count > 0)
            {
                PlaySound(_getHitClips);
            }
        }

        public void PlayDeathClip()
        {
            if (_deathClips.Count > 0)
            {
                PlaySound(_deathClips);
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