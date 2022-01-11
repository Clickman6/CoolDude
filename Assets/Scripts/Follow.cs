using Managers;
using Player;
using UnityEngine;

public class Follow : MonoBehaviour {
    private Camera _camera;
    private Controller _target;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lerpRate;

    private void Awake() {
        _camera = Camera.main;
        _target = Base.Movement;
    }

    private void Update() {
        if (GameManager.IsPause) return;
        
        SetTargetPosition();
    }

    private void SetTargetPosition() {
        Vector3 position = _camera.WorldToScreenPoint(_target.transform.position);
        Vector3 offset = _offset;

        offset.x *= -_target.RightConst;

        Vector3 target = _camera.ScreenToWorldPoint(position - offset);

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * _lerpRate);
    }
}
