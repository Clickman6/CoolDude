using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsOnTrigger : MonoBehaviour {
    [SerializeField] private Collider[] _colliders;

    [SerializeField] private UnityEvent<Collider> EventOnTriggerEnter;
    [SerializeField] private UnityEvent<Collider> EventOnTriggerStay;
    [SerializeField] private UnityEvent<Collider> EventOnTriggerExit;

    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < _colliders.Length; i++) {
            if (_colliders[i] == other) {
                EventOnTriggerEnter.Invoke(other);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        for (int i = 0; i < _colliders.Length; i++) {
            if (_colliders[i] == other) {
                EventOnTriggerStay.Invoke(other);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        for (int i = 0; i < _colliders.Length; i++) {
            if (_colliders[i] == other) {
                EventOnTriggerExit.Invoke(other);
            }
        }
    }
}
