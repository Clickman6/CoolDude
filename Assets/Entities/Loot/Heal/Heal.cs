using Managers;
using Player;
using UnityEngine;

namespace Loot {
    [RequireComponent(typeof(Collider))]
    public class Heal : MonoBehaviour {

        [SerializeField] private int _healthValue = 1;

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth player)) {
                player.AddHealth(_healthValue);
                AudioManager.Instance.PlayPickUpLoot();
                Die();
            }
        }

        public void Die() {
            Destroy(gameObject);
        }
    }
}
