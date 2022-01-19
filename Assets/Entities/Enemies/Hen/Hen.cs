using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hen : MonoBehaviour {
    private Rigidbody _rb;
    private Transform _player;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _accelerationTime = 1f;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _player = PlayerBase.Transform;
    }

    private void FixedUpdate() {
        if (GameManager.IsPause) return;
        
        Vector3 direction = (_player.position - transform.position).normalized;

        Vector3 force = _rb.mass * (direction * _speed - _rb.velocity) / _accelerationTime;
        
        _rb.AddForce(force);
    }
}
