using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

    [SerializeField] private float _period = 7f;
    [SerializeField] private Animator _animator;

    private float _timer;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Update() {
        _timer += Time.deltaTime;

        if (_timer > _period) {
            _timer = 0f;
            _animator.SetTrigger(Attack);
        }
    }
}
