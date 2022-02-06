using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class RopeGun : MonoBehaviour {
    [SerializeField] private Hook _hook;
    [SerializeField] private RopeRenderer _ropeRenderer;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _ropePivot;
    [SerializeField] private UnityEvent _onAttached;

    private SpringJoint _springJoint;
    private float _length;
    private RopeState _currentRopeState;

    private void Start() {
        Detach();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Mouse2)) {
            Shot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _currentRopeState == RopeState.Enable) {
            Detach();
        }

        if (_currentRopeState == RopeState.Fly) {
            if (Distance() > 20f) {
                Detach();
            }
        }

        if (_currentRopeState != RopeState.Disable) {
            _ropeRenderer.Draw(_ropePivot.position, _hook.transform.position, _length);
        }
    }

    private void Shot() {
        Detach();
        _hook.Activate();

        _hook.transform.position = _spawn.position;
        _hook.transform.rotation = _spawn.rotation;
        _hook.Rigidbody.velocity = _spawn.forward * _speed;

        _currentRopeState = RopeState.Fly;
    }

    public void PullPlayer() {
        if (_springJoint) return;

        _springJoint = PlayerBase.GameObject.AddComponent<SpringJoint>();

        _springJoint.connectedBody = _hook.Rigidbody;
        _springJoint.autoConfigureConnectedAnchor = false;
        _springJoint.connectedAnchor = Vector3.zero;
        _springJoint.anchor = _ropePivot.localPosition;
        _springJoint.spring = 100f;
        _springJoint.damper = 5f;

        _length = Distance();
        _springJoint.maxDistance = _length;

        _currentRopeState = RopeState.Enable;

        _onAttached.Invoke();
    }

    private void Detach() {
        if (_springJoint) Destroy(_springJoint);

        _length = 0f;
        _hook.Deactivate();
        _currentRopeState = RopeState.Disable;
        _ropeRenderer.Hide();
    }

    private float Distance() {
        return Vector3.Distance(_ropePivot.position, _hook.transform.position);
    }
}

public enum RopeState {
    Fly,
    Enable,
    Disable
}
