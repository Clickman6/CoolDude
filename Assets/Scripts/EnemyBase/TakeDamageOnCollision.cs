using System;
using Player;
using UnityEngine;

namespace EnemyBase {
    [RequireComponent(typeof(EnemyHealth))]
    public class TakeDamageOnCollision : MonoBehaviour {
        private EnemyHealth _health;
        [SerializeField] private bool _dieOnAnyCollision;

        private void Start() {
            _health = GetComponent<EnemyHealth>();
        }

        private void OnCollisionEnter(Collision other) {
            if (other.rigidbody && other.rigidbody.TryGetComponent(out Bullet bullet)) {
                _health.TakeDamage(bullet.Damage);
            }

            if (_dieOnAnyCollision) {
                _health.TakeDamage(_health.Health + 1);
            }
        }
    }
}
