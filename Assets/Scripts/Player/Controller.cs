using System.Collections;
using UnityEngine;

namespace Player {
    public class Controller : MonoBehaviour {
        public static Controller Instance { get; private set; }

        private Rigidbody _rb;

        [Header("Movable")]
        private int _isRight = 1;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _friction;

        [Header("Jump")]
        private bool _jumpControl;
        private float _jumpTime;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpControlTime;

        [Header("Ground")]
        private bool _isGrounded;

        [Header("Other")]
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private float _angleDirection;

        // Properties
        public int  IsRight    => _isRight;
        public bool IsGrounded => _isGrounded;

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update() {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !_isGrounded) {
                SetSneak();
            } else {
                UnsetSneak();
            }
        }

        private void FixedUpdate() {
            Move();
            Jump();
        }

        private void Move() {
            float speedMultiplier = _isGrounded ? 1f : 0.1f;
            float h = Input.GetAxis("Horizontal");
            Vector3 movement = Vector3.right * _speed * h;

            if (IsGrounded) {
                if ((h > 0 && IsRight != 1) || (h < 0 && IsRight != -1)) {
                    ChangeDirection();
                }
            }

            if (_rb.velocity.x > _maxSpeed && h > 0) {
                speedMultiplier = 0f;
            }

            if (_rb.velocity.x < -_maxSpeed && h < 0) {
                speedMultiplier = 0f;
            }

            _rb.AddForce(movement * speedMultiplier, ForceMode.VelocityChange);

            if (_isGrounded) {
                _rb.AddForce(-Vector3.right * _rb.velocity.x * _friction, ForceMode.VelocityChange);
            }
        }

        private void Jump() {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
                if (_isGrounded) _jumpControl = true;
            } else {
                _jumpControl = false;
            }

            if (_jumpControl) {
                _jumpTime += Time.fixedDeltaTime;

                if (_jumpTime < _jumpControlTime) {
                    _rb.AddForce(transform.up * _jumpForce / (_jumpTime * 10), ForceMode.VelocityChange);
                }
            } else {
                _jumpTime = 0;
            }
        }

        private void SetSneak() {
            _bodyTransform.localScale =
                Vector3.Lerp(_bodyTransform.localScale, new Vector3(1f, 0.5f, 1f), Time.deltaTime * 15f);
        }

        private void UnsetSneak() {
            _bodyTransform.localScale = Vector3.Lerp(_bodyTransform.localScale, Vector3.one, Time.deltaTime * 15f);
        }

        public void ChangeDirection() {
            _isRight *= -1;

            StopAllCoroutines();
            StartCoroutine(RotationBody());
        }

        private IEnumerator RotationBody() {
            while (true) {
                _bodyTransform.eulerAngles = Vector3.up *
                                             Mathf.LerpAngle(_bodyTransform.eulerAngles.y, -IsRight * _angleDirection,
                                                             Time.deltaTime * 15f);
                yield return null;
            }
        }

        private void OnCollisionStay(Collision other) {
            for (int i = 0; i < other.contactCount; i++) {
                float angle = Vector3.Angle(other.contacts[i].normal, Vector3.up);

                if (angle < 45f) _isGrounded = true;
            }
        }

        private void OnCollisionExit(Collision other) {
            _isGrounded = false;
        }
    }
}
