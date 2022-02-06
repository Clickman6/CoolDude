using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Hook : MonoBehaviour {
    public Rigidbody Rigidbody { get; private set; }

    private FixedJoint _fixedJoint;
    private Collider _collider;
    [SerializeField] private Collider[] _ignoredColliders;

    [Header("Events")]
    public UnityEvent OnHookAttached;

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        for (int i = 0; i < _ignoredColliders.Length; i++) {
            Physics.IgnoreCollision(_collider, _ignoredColliders[i]);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (_fixedJoint) return;

        _fixedJoint = gameObject.AddComponent<FixedJoint>();
        if (other.rigidbody) _fixedJoint.connectedBody = other.rigidbody;

        OnHookAttached.Invoke();
    }

    private void Detach() {
        if (_fixedJoint) Destroy(_fixedJoint);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        Detach();
        gameObject.SetActive(false);
    }
}
