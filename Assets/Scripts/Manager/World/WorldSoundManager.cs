using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class WorldSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _SFXSource;

        [SerializeField] private float _ambientChangeVolumeSpeed = 0.5f;
        [SerializeField] private AudioClip _mainMenuAmbientClip;
        [SerializeField] private AudioClip _worldAmbientClip;

        [SerializeField] private List<TextureSound> _footstepClips = new();

        private Coroutine _changeAmbientCoroutine;

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
            _SFXSource.volume = volume;
            _SFXSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;
            _SFXSource.PlayOneShot(clip);
        }

        public void ChangeAmbient(AudioClip clip = null)
        {
            if (clip == null)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    clip = _mainMenuAmbientClip;
                }
                else
                {
                    clip = _worldAmbientClip;
                }
            }
            if (_changeAmbientCoroutine != null)
            {
                StopCoroutine(_changeAmbientCoroutine);
            }
            _changeAmbientCoroutine = StartCoroutine(ChangeAmbientTimer(clip));
        }

        private IEnumerator ChangeAmbientTimer(AudioClip clip)
        {
            while (_ambientSource.volume != 0f)
            {
                _ambientSource.volume -= _ambientChangeVolumeSpeed * Time.deltaTime;
                yield return null;
            }
            _ambientSource.volume = 0f;
            _ambientSource.clip = clip;
            _ambientSource.Play();
            while (_ambientSource.volume != 1f)
            {
                _ambientSource.volume += _ambientChangeVolumeSpeed * Time.deltaTime;
                yield return null;
            }
            _ambientSource.volume = 1f;
            _changeAmbientCoroutine = null;
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