using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager> {

        [Header("Simple sounds")]
        private AudioSource _audioSource;
        [SerializeField] private CustomAudio _shot;
        [SerializeField] private CustomAudio _pickUpLoot;
        [SerializeField] private CustomAudio _playerHit;
        [SerializeField] private CustomAudio _enemyHit;

        protected override void Awake() {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void PlayShot(float      pitch) => PlaySound(_shot, pitch);
        public void PlayPlayerHit(float pitch) => PlaySound(_playerHit, pitch);

        public void PlayEnemyHit()   => PlaySound(_enemyHit, Random.Range(0.75f, 1.25f));
        public void PlayPickUpLoot() => PlaySound(_pickUpLoot);

        private void PlaySound(CustomAudio audio, float pitch = 1f) {
            if (audio == null) return;

            _audioSource.pitch = pitch;

            _audioSource.PlayOneShot(audio.Clip, audio.Volume);
        }
    }
}
