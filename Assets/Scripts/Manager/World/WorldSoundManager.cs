using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace WinterUniverse
{
    public class WorldSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _soundSource;

        [SerializeField] private float _ambientFadeSpeed = 0.5f;
        [SerializeField] private List<AudioClip> _ambientClips = new();

        [SerializeField] private List<TextureSound> _footstepClips = new();

        private Coroutine _changeAmbientCoroutine;

        public void Initialize()
        {
            ChangeAmbient();
        }

        public void SetMasterVolume(float value)
        {
            _audioMixer.SetFloat("VolumeMaster", value);
        }

        public void SetAmbientVolume(float value)
        {
            _audioMixer.SetFloat("VolumeAmbient", value);
        }

        public void SetSoundVolume(float value)
        {
            _audioMixer.SetFloat("VolumeSound", value);
        }

        public AudioClip ChooseRandomClip(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }

        public void PlaySFX(AudioClip clip, bool randomizePitch = true, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (clip == null)
            {
                return;
            }
            _soundSource.volume = volume;
            _soundSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;
            _soundSource.PlayOneShot(clip);
        }

        public void ChangeAmbient()
        {
            ChangeAmbient(_ambientClips);
        }

        public void ChangeAmbient(List<AudioClip> clips)
        {
            if (_changeAmbientCoroutine != null)
            {
                StopCoroutine(_changeAmbientCoroutine);
            }
            _changeAmbientCoroutine = StartCoroutine(PlayAmbientTimer(clips));
        }

        private IEnumerator PlayAmbientTimer(List<AudioClip> clips)
        {
            WaitForSeconds delay = new(5f);
            while (true)
            {
                while (_ambientSource.volume != 0f)
                {
                    _ambientSource.volume -= _ambientFadeSpeed * Time.deltaTime;
                    yield return null;
                }
                _ambientSource.volume = 0f;
                _ambientSource.clip = clips[Random.Range(0, clips.Count)];
                _ambientSource.Play();
                while (_ambientSource.volume != 1f)
                {
                    _ambientSource.volume += _ambientFadeSpeed * Time.deltaTime;
                    yield return null;
                }
                _ambientSource.volume = 1f;
                while (_ambientSource.isPlaying)
                {
                    yield return delay;
                }
            }
        }

        public AudioClip GetFootstepClip(Transform ground)
        {
            //if (ground.TryGetComponent(out SurfaceManager sm))
            //{

            //}
            //if (ground.TryGetComponent(out Terrain terrain))
            //{

            //}
            if (ground.TryGetComponent(out MeshRenderer meshRenderer))
            {
                foreach (TextureSound ts in _footstepClips)
                {
                    if (meshRenderer.material.mainTexture == ts.Texture)
                    {
                        return ChooseRandomClip(ts.Clips);
                    }
                }
            }
            return null;
        }
    }
}