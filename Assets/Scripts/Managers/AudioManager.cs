using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }

        [Header("Simple sounds")]
        private AudioSource _audioSource;
        [SerializeField] private CustomAudio _shot;

        private void Awake() {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void PlayShot(float pitch) => PlaySound(_shot, pitch);

        private void PlaySound(CustomAudio audio, float pitch = 1f) {
            if (audio == null) return;

            _audioSource.pitch = pitch;

            _audioSource.PlayOneShot(audio.Clip, audio.Volume);
        }
    }
}
