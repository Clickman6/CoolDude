using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUpWeapon : MonoBehaviour {
    [SerializeField] private int _gunID;
    [SerializeField] private int _numberOfBullets;

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Weapons weapons)) {
            weapons.PickUpWeapon(_gunID, _numberOfBullets);

            AudioManager.Instance.PlayPickUpLoot();
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
