using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager> {
        [Header("Simple sounds")]
        private AudioSource _audioSource;
        [SerializeField] private CustomAudio _pickUpLoot;
        [SerializeField] private CustomAudio _playerHit;
        [SerializeField] private CustomAudio _enemyHit;
        [SerializeField] private AudioSource _backgroundMusic;

        protected override void Awake() {
            base.Awake();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void PlayPlayerHit(float pitch) => PlaySound(_playerHit, pitch);

        public void PlayEnemyHit()   => PlaySound(_enemyHit, Random.Range(0.75f, 1.25f));
        public void PlayPickUpLoot() => PlaySound(_pickUpLoot);

        private void PlaySound(CustomAudio audio, float pitch = 1f) {
            if (audio == null) return;

            _audioSource.pitch = pitch;

            _audioSource.PlayOneShot(audio.Clip, audio.Volume);
        }

        public void ToggleBackgroundMusic(bool value) {
            _backgroundMusic.enabled = value;
        }

        public void ChangeGlobalVolume(float volume) {
            AudioListener.volume = volume;
        }
    }
}
