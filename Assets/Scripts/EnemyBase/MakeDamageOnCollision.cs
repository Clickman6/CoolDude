using Player;
using UnityEngine;

namespace EnemyBase {
    public class MakeDamageOnCollision : MonoBehaviour {
        [SerializeField] private int _damage = 1;
        public int Damage => _damage;

        private void OnCollisionEnter(Collision other) {
            if (other.rigidbody && other.rigidbody.TryGetComponent(out PlayerHealth health)) {
                health.TakeDamage(Damage);
            }
        }
    }
}
