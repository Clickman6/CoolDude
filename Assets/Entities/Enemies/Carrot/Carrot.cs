using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Carrot : MonoBehaviour {

    private Rigidbody _rb;
    private Transform _player;

    [SerializeField] private float _speed;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _player = PlayerBase.Transform;

        // Для полета перпендикулярно оси z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.rotation = Quaternion.identity;

        Vector3 direction = (_player.position - transform.position).normalized;

        _rb.velocity = direction * _speed;
    }
}
