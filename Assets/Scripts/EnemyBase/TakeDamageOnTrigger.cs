using System;
using UnityEngine;

namespace EnemyBase {
    public class TakeDamageOnTrigger : MonoBehaviour {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private bool _dieOnAnyTrigger;

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Bullet bullet)) {
                _health.TakeDamage(bullet.Damage);
                bullet.Die();
            }

            if (_dieOnAnyTrigger && !other.isTrigger) {
                _health.TakeDamage(_health.Health + 1);
            }
        }
    }
}
