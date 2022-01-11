using UnityEngine;

public class CarrotCreater : MonoBehaviour {
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawn;
    
    public void CreateCarrot() {
        Instantiate(_prefab, _spawn.position, Quaternion.identity);
    }
}
