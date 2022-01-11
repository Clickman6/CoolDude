using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class HealthUI : MonoBehaviour {
        [SerializeField] private GameObject _heart;

        private List<GameObject> _hearts = new List<GameObject>();

        private void Start() {
            Init(Player.Base.Health.MaxHealth);
            UpdateHealth(Player.Base.Health.Health);
        }

        public void Init(int health) {
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
