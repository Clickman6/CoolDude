using System;
using UnityEngine;

namespace Managers {
    public class TimeManager : MonoBehaviour {
        private float _startFixedDeltaTime;
        [SerializeField] private float _timeScale = 0.3f;

        private void Start() {
            _startFixedDeltaTime = Time.fixedDeltaTime;
        }

        private void Update() {
            if (GameManager.IsPause) return;

            if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.E)) {
                Time.timeScale = _timeScale;
            } else {
                Time.timeScale = 1f;
            }

            Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
        }
    }
}
