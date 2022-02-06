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
        private float _jumpFrames;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpControlFrames;

        [Header("Ground")]
        private bool _isGrounded;

        [Header("Other")]
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private float _angleDirection;

        // Properties
        public int  RightConst => _isRight ? 1 : -1;
        public bool IsGrounded => _isGrounded;

        private void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update() {
            if (GameManager.IsPause) return;

            if (Input.GetKey(KeyCode.LeftControl) || !_isGrounded) {
                SetSneak();
            } else {
                UnsetSneak();
            }

            Jump();
        }

        private void FixedUpdate() {
            Move();
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

                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 20f);
            }
        }

        private void Jump() {
            if (Input.GetKeyDown(KeyCode.Space) && _canJump) {
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

                _jumpControl = true;
                _canJump = false;

                _rb.freezeRotation = false;
                _rb.AddRelativeTorque(0f, 0f, 10f * -RightConst, ForceMode.VelocityChange);
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                _jumpControl = false;
            }

            if (_jumpControl) {
                if (_jumpFrames++ < _jumpControlFrames) {
                    _rb.AddForce(Vector3.up * _jumpForce / _jumpFrames,
                                 ForceMode.VelocityChange);

                } else {
                    _jumpControl = false;
                }
            } else {
                _jumpFrames = 0;
            }
        }

        // Для прыжка с веревки
        public void ResetJump() {
            _canJump = true;
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

        /*
         * Этот код для 1 прыжка в любой момент после колизии, если прописать в OnCollisionStay,
         * то сначала Jump() установит _canJump = false, а потом выполнится OnCollisionStay,
         * который заменит _canJump на true (из-за чего получается двойной прыжок)
         * и только потом будет OnCollisionExit 
         */
        private void OnCollisionEnter(Collision other) {
            CheckStayGround(other);

            if (_isGrounded) {
                _canJump = true;

                _rb.freezeRotation = true;
            }
        }

        private void OnCollisionStay(Collision other) {
            CheckStayGround(other);
        }

        private void OnCollisionExit(Collision other) {
            _isGrounded = false;
        }

        private void CheckStayGround(Collision other) {
            for (int i = 0; i < other.contactCount; i++) {
                float angle = Vector3.Angle(other.contacts[i].normal, Vector3.up);

                if (angle <= 45f) {
                    _isGrounded = true;
                }
            }
        }
    }
}
