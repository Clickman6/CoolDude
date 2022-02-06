using Managers;
using Player;
using UnityEngine;

[ExecuteAlways]
public class FollowCamera : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private Control _target;

    [SerializeField] private Vector3 _offsetInViewportPoint;
    [SerializeField] private float _lerpRate;
    [SerializeField] private float _lerpRateY;

    private void LateUpdate() {
        if (GameManager.IsPause) return;

        SetTargetPosition();
    }

    private void SetTargetPosition() {
        Vector3 position = _camera.WorldToViewportPoint(_target.transform.position);

        Vector3 offset = _offsetInViewportPoint;
        offset.x *= -_target.RightConst;

        Vector3 target = _camera.ViewportToWorldPoint(position - offset);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, Time.deltaTime * _lerpRate),
                                         Mathf.Lerp(transform.position.y, target.y, Time.deltaTime * _lerpRateY),
                                         Mathf.Lerp(transform.position.z, target.z, Time.deltaTime * _lerpRate));
        // Vector3.Lerp(transform.position, target, Time.deltaTime * _lerpRate);
    }
}
