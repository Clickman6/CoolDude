using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace EnemyBase {
    public class EnemyHealth : MonoBehaviour {
        [SerializeField] private int _health = 1;

        public UnityEvent<int> EventOnTakeDamage;
        public UnityEvent EventOnDie;

        public int Health => _health;

        public void TakeDamage(int value) {
            _health -= value;

            if (Health <= 0) {
                _health = 0;
                Die();
            }

            EventOnTakeDamage.Invoke(Health);
        }

        public void Die() {
            Destroy(gameObject);
            EventOnDie.Invoke();
        }
    }
}
