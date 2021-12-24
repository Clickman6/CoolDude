using UnityEngine;

namespace Player {
    public class Pointer : MonoBehaviour {
        private Controller Player => Controller.Instance;

        private Camera _camera;
        [SerializeField] private Transform _aim;
        [SerializeField] private float _maxAimDistance;

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(-Vector3.forward, Vector3.zero);

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

            plane.Raycast(ray, out float distance);
            Vector3 point = ray.GetPoint(distance);

            SetAim(point, out Vector3 target);

            if (!Player.IsGrounded) {
                SetPlayerRotation(target);
            }
        }

        private void SetPlayerRotation(Vector3 target) {
            if (target.normalized.x != 0 && Mathf.Sign(target.normalized.x) != Player.IsRight) {
                Player.ChangeDirection();
            }
        }

        private void SetAim(Vector3 point, out Vector3 target) {
            target = point - transform.position;

            if (target.magnitude > _maxAimDistance) {
                _aim.position = transform.position + target.normalized * _maxAimDistance;
            } else {
                _aim.position = point;
            }

            transform.rotation = Quaternion.LookRotation(target);
        }
    }
}
