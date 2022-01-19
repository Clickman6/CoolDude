using UnityEngine;

public class MoveToPivot : MonoBehaviour {
    [SerializeField] private Transform _target;

    public void Update() {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }
}
