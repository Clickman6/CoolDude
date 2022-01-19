using Managers;
using Player;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    private Camera _camera;
    private Control _target;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lerpRate;

    private void Start() {
        _camera = Camera.main;
        _target = PlayerBase.Movement;
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
