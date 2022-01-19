using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ActivateByDistance : MonoBehaviour {
    [SerializeField] private float _distanceToActivate = 20f;
    private bool _isActive;

    private void Start() {
        EnemyController.Instance.AddEnemy(this);
        _isActive = enabled;
    }

    public void CheckDistance(Vector3 player) {
        float distance = Vector3.Distance(transform.position, player);

        if (!_isActive && distance < _distanceToActivate) {
            Activate();
        } else if (_isActive && distance > _distanceToActivate + 2f) {
            Deactivate();
        }
    }

    private void Activate() {
        _isActive = true;
        gameObject.SetActive(_isActive);
    }

    private void Deactivate() {
        _isActive = false;
        gameObject.SetActive(_isActive);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.DrawWireDisc(transform.position, Vector3.forward, _distanceToActivate);
    }
#endif

    private void OnDestroy() {
        EnemyController.Instance.DeleteEnemy(this);
    }
}
