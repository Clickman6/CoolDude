using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

public class Pistol : Gun {
    protected override void Update() {
        if (GameManager.IsPause) return;

        _timer += Time.unscaledDeltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _timer > _shotPeriod) {
            _timer = 0;

            Shot();
        }
    }
}
