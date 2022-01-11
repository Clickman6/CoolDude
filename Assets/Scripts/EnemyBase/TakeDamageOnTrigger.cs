using System;
using UnityEngine;

namespace EnemyBase {
    [RequireComponent(typeof(EnemyHealth))]
    public class TakeDamageOnTrigger : MonoBehaviour {
        private EnemyHealth _health;
        [SerializeField] private bool _dieOnAnyTrigger;

        private void Start() {
            _health = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out Bullet bullet)) {
                _health.TakeDamage(bullet.Damage);
            }

            if (_dieOnAnyTrigger) {
                _health.TakeDamage(_health.Health + 1);
            }
        }
    }
}
