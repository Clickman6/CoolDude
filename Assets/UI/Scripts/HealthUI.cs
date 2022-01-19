using System;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace UI {
    public class HealthUI : MonoBehaviour {
        [SerializeField] private GameObject _heart;

        private readonly List<GameObject> _hearts = new List<GameObject>();

        private void Start() {
            Init(PlayerBase.Health.MaxHealth);
            UpdateHealth(PlayerBase.Health.Health);
        }

        private void Init(int health) {
            for (int i = 0; i < health; i++) {
                _hearts.Add(Instantiate(_heart, transform));
            }
        }

        public void UpdateHealth(int value) {
            for (int i = 0; i < _hearts.Count; i++) {
                _hearts[i].SetActive(i < value);
            }
        }
    }
}
