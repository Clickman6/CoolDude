using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Acorn : MonoBehaviour {

    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _maxRotationSpeed;

    private void Start() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddRelativeForce(_velocity, ForceMode.VelocityChange);

        rigidbody.angularVelocity = new Vector3(Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
                                                Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
                                                Random.Range(-_maxRotationSpeed, _maxRotationSpeed));

    }
}
