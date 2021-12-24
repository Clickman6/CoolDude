using Player;
using UnityEngine;

public class Follow : MonoBehaviour {
    private Camera _camera;
    [SerializeField] private Controller _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lerpRate;

    private void Awake() {
        _camera = Camera.main;
    }

    private void Update() {
        SetTargetPosition();
    }

    private void SetTargetPosition() {
        Vector3 position = _camera.WorldToScreenPoint(_target.transform.position);
        Vector3 offset = _offset;

        offset.x *= -_target.IsRight;

        Vector3 target = _camera.ScreenToWorldPoint(position - offset);

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * _lerpRate);
    }
}
