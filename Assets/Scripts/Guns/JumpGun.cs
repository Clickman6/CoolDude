using System;
using Managers;
using UnityEngine;

namespace Guns {
    public class JumpGun : MonoBehaviour {
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Weapons _weapons;

        [Header("Charge Params")]
        [SerializeField] private float _maxCharge;
        private float _currentCharge;
        private bool _isCharged = true;

        [Header("UI")]
        [SerializeField] private ChargeIcon _chargeIcon;

        private void Start() {
            _currentCharge = _maxCharge;
        }

        private void Update() {
            if (GameManager.IsPause) return;

            if (_isCharged) {
                if (Input.GetKeyDown(KeyCode.LeftShift)) {
                    _rb.velocity = Vector3.zero;
                    _rb.AddForce(-transform.forward * _speed, ForceMode.VelocityChange);
                    _weapons.GetCurrentGun.Shot();

                    _currentCharge = 0;
                    _isCharged = false;
                    _chargeIcon.StartCharge();
                }
            } else {
                _currentCharge += Time.unscaledDeltaTime;
                _chargeIcon.ChangeCharge(_currentCharge, _maxCharge);

                if (_currentCharge >= _maxCharge) {
                    _isCharged = true;
                    _chargeIcon.StopCharge();
                }
            }
        }
    }
}
