using System;
using Player;
using UnityEngine;

namespace EnemyBase {
    public class MakeDamageOnTrigger : MonoBehaviour {
        [SerializeField] private int _damage = 1;
        public int Damage => _damage;

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth health)) {
                health.TakeDamage(Damage);
            }
        }
    }
}
