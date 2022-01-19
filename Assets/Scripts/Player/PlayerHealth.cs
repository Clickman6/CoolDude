using System;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Player {
    public class PlayerHealth : MonoBehaviour {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        private bool _isInvulnerable;

        public int Health    => _health;
        public int MaxHealth => _maxHealth;

        public UnityEvent<int> EventOnTakeDamage;
        public UnityEvent<int> EventOnAddHealth;
        public UnityEvent EventOnDie;

        public void TakeDamage(int value) {
            if (_isInvulnerable) return;

            _health -= value;

            if (Health <= 0) {
                _health = 0;
                Die();

                return;
            }

            _isInvulnerable = true;
            Invoke(nameof(ResetInvulnerable), 1f);

            EventOnTakeDamage.Invoke(Health);
            AudioManager.Instance.PlayPlayerHit(Random.Range(0.75f, 1.25f));
        }

        public void AddHealth(int value) {
            _health += value;

            if (Health > MaxHealth) {
                _health = MaxHealth;
            }

            EventOnAddHealth.Invoke(Health);
        }

        private void ResetInvulnerable() {
            _isInvulnerable = false;
        }

        public void Die() {
            EventOnDie.Invoke();
            gameObject.SetActive(false);
        }
    }
}
