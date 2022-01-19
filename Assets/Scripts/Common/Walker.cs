using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walker : MonoBehaviour {
    [SerializeField] private Transform _targetLeft;
    [SerializeField] private Transform _targetRight;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopTime;
    [SerializeField] private bool _isStopped;

    [SerializeField] private Direction _currentDirection;

    [SerializeField] private Transform _rayStart;

    [Header("Events")]
    [SerializeField] private UnityEvent _eventOnLeftTarget;
    [SerializeField] private UnityEvent _eventOnRightTarget;

    private void Start() {
        _targetLeft.parent = null;
        _targetRight.parent = null;
    }

    private void Update() {
        if (_isStopped) return;

        Walk();

        if (Physics.Raycast(_rayStart.position, Vector3.down, out RaycastHit hit)) {
            transform.position = hit.point;
        }
    }

    private void Walk() {
        if (_currentDirection == Direction.Left) {
            transform.position -= new Vector3(Time.deltaTime * _speed, 0f, 0f);

            if (transform.position.x < _targetLeft.position.x) {
                _currentDirection = Direction.Right;
                _isStopped = true;

                Invoke(nameof(ContinueWalk), _stopTime);
                _eventOnLeftTarget.Invoke();
            }
        } else {
            transform.position += new Vector3(Time.deltaTime * _speed, 0f, 0f);

            if (transform.position.x > _targetRight.position.x) {
                _currentDirection = Direction.Left;
                _isStopped = true;

                Invoke(nameof(ContinueWalk), _stopTime);
                _eventOnRightTarget.Invoke();
            }
        }
    }

    public void ContinueWalk() {
        _isStopped = false;
    }

    private enum Direction {
        Left,
        Right
    }
}
