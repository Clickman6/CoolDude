using System;
using Managers;
using Player;
using UnityEngine;

public class Rocket : MonoBehaviour {
    private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private void Start() {
        _target = PlayerBase.Transform;

        // Для полета перпендикулярно оси z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        Vector3 direction = (_target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
    }

    private void Update() {
        if (GameManager.IsPause) return;

        Vector3 direction = (_target.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
