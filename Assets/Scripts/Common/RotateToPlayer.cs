using System;
using Managers;
using Player;
using UnityEngine;

namespace Common {
    public class RotateToPlayer : MonoBehaviour {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Vector3 _rightEuler;
        [SerializeField] private Vector3 _leftEuler;

        private Transform _player;
        private Vector3 _targetEuler;

        private void Start() {
            _player = PlayerBase.Transform;
        }

        private void Update() {
            if(GameManager.IsPause) return;
            
            if (transform.position.x < _player.position.x) {
                _targetEuler = _rightEuler;
            } else {
                _targetEuler = _leftEuler;
            }

            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _speed);
        }
    }
}
