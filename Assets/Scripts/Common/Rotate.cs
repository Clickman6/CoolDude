using System;
using UnityEngine;

namespace Common {
    public class Rotate : MonoBehaviour {
        [SerializeField] private Vector3 _speed;

        private void Update() {
            transform.Rotate(_speed * Time.deltaTime);
        }
    }
}
