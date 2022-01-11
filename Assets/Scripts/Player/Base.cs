using UnityEngine;

namespace Player {

    [RequireComponent(typeof(Controller), typeof(PlayerHealth))]
    public class Base : Singleton<Base> {
        private Controller _controller;
        private PlayerHealth _playerHealth;

        public static Controller   Movement => Instance._controller;
        public static PlayerHealth Health   => Instance._playerHealth;

        protected override void Awake() {
            _controller = GetComponent<Controller>();
            _playerHealth = GetComponent<PlayerHealth>();
        }
    }
}
