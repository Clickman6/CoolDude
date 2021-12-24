using UnityEngine;

namespace Player {
    public class Bullet : MonoBehaviour {
        [SerializeField] private GameObject _effectPrefab;

        private void Start() {
            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter(Collision other) {
            Die();
        }

        private void Die() {
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
