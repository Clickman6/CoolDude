using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    [SerializeField] private Gun[] _guns;
    [SerializeField] private int _currentIndex;

    public Gun GetCurrentGun => _guns[_currentIndex];
    
    private void Start() {
        TakeGunByIndex(_currentIndex);
    }

    public void TakeGunByIndex(int index = 0) {
        _currentIndex = index;

        for (int i = 0; i < _guns.Length; i++) {
            if (i == index) {
                _guns[i].Activate();
            } else {
                _guns[i].Deactivate();
            }
        }
    }

    public void PickUpWeapon(int index, int numberOfBullets) {
        TakeGunByIndex(index);
        
        _guns[index].PickUpLoot(numberOfBullets);
    }
}
