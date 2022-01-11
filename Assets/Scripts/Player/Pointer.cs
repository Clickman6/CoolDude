using UnityEngine;

namespace Player {
    public class Pointer : MonoBehaviour {
        private Controller _player;
        private Camera _camera;
        [SerializeField] private Transform _aim;
        [SerializeField] private float _maxAimDistance;

        private void Start() {
            _camera = Camera.main;
            _player = Base.Movement;
        }

        private void LateUpdate() {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(-Vector3.forward, Vector3.zero);

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

            plane.Raycast(ray, out float distance);
            Vector3 point = ray.GetPoint(distance);

            SetAim(point, out Vector3 target);

            if (!_player.IsGrounded) {
                SetPlayerDirection(target);
            }
        }

        private void SetPlayerDirection(Vector3 target) {
            if (target.normalized.x != 0 && Mathf.Sign(target.normalized.x) != _player.RightConst) {
                _player.ChangeDirection();
            }
        }

        private void SetAim(Vector3 point, out Vector3 target) {
            target = point - transform.position;

            float magnitude = Mathf.Clamp(target.magnitude, 0, _maxAimDistance);

            _aim.position = transform.position + target.normalized * magnitude;

            transform.LookAt(point);
        }
    }
}
