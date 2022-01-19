using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class RotateToTargetEuler : MonoBehaviour {
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3 _rightEuler;
    [SerializeField] private Vector3 _leftEuler;

    private Vector3 _targetEuler;

    private void Update() {
        transform.localRotation =
            Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _speed);
    }

    public void RotateToLeft() {
        _targetEuler = _leftEuler;
    }

    public void RotateToRight() {
        _targetEuler = _rightEuler;
    }
}
