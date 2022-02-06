using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision : MonoBehaviour {

    [SerializeField] private UnityEvent OnColliderEnter;

    private void OnCollisionEnter(Collision other) {
        if (other.rigidbody && other.rigidbody.GetComponent<PlayerBase>()) {
            OnColliderEnter.Invoke();
        }
    }
}
