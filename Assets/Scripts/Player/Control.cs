using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Player {
    public class Control : MonoBehaviour {
        private Coroutine _rotateCoroutine;

        private Rigidbody _rb;

        [Header("Movable")]
        private bool _isRight = true;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _friction;

        [Header("Jump")]
        private bool _canJump;
        private bool _jumpControl;
        private float _jumpTime;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpControlTime;

        [Header("Ground")]
        private bool _isGrounded;

        [Header("Other")]
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private float _angleDirection;

        [SerializeField] private AnimationCurve _curve; // todo delete

        // Properties
        public int  RightConst => _isRight ? 1 : -1;
        public bool IsGrounded => _isGrounded;

        private void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update() {
            if (GameManager.IsPause) return;

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !_isGrounded) {
                SetSneak();
            } else {
                UnsetSneak();
            }

            Keyframe keyframe = new Keyframe(Time.time, Mathf.Abs(_rb.velocity.x));

            _curve.AddKey(keyframe);
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
                if (h != 0 && Mathf.Sign(h) != RightConst) {
                    ChangeDirection();
                }
            } else {
                if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(_maxSpeed)) {
                    speedMultiplier = 0f;
                }
            }

            _rb.AddForce(movement * speedMultiplier, ForceMode.VelocityChange);

            if (_isGrounded) {
                _rb.AddForce(-Vector3.right * _rb.velocity.x * _friction, ForceMode.VelocityChange);
            }
        }

        private void Jump() {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
                if (_canJump) _jumpControl = true;

                _canJump = false;
            } else {
                _jumpControl = false;
            }

            if (_jumpControl) {
                _jumpTime += Time.fixedUnscaledDeltaTime;

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
            _isRight = !_isRight;

            if (_rotateCoroutine != null) {
                StopCoroutine(_rotateCoroutine);
            }

            _rotateCoroutine = StartCoroutine(RotationBody());
        }

        private IEnumerator RotationBody() {
            while (true) {
                _bodyTransform.localEulerAngles = Vector3.up *
                                                  Mathf.LerpAngle(_bodyTransform.localEulerAngles.y,
                                                                  -RightConst * _angleDirection,
                                                                  Time.deltaTime * 15f);
                yield return null;
            }
        }

        private void OnCollisionStay(Collision other) {
            for (int i = 0; i < other.contactCount; i++) {
                float angle = Vector3.Angle(other.contacts[i].normal, Vector3.up);

                if (angle <= 45f) {
                    _isGrounded = true;
                    _canJump = true;
                }
            }
        }

        private void OnCollisionExit(Collision other) {
            _isGrounded = false;
        }
    }
}
