using Managers;
using Player;
using UnityEngine;

namespace Loot.Heal {
    public class Heal : MonoBehaviour {

        [SerializeField] private int _healthValue = 1;

        private void OnTriggerEnter(Collider other) {
            if (other.attachedRigidbody.TryGetComponent(out PlayerHealth player)) {
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
