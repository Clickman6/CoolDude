using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _effectPrefab;

    public int Damage => _damage;

    private void Start() {
        Invoke(nameof(Die), 5f);
    }

    private void OnCollisionEnter(Collision other) {
        Die();
    }

    private void Die() {
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
