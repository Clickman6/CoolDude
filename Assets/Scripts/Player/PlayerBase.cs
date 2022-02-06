using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Control), typeof(PlayerHealth))]
    public class PlayerBase : Singleton<PlayerBase> {

        private Control _movement;
        private PlayerHealth _playerHealth;

        public static Control      Movement   => Instance._movement;
        public static PlayerHealth Health     => Instance._playerHealth;
        public static Transform    Transform  => Instance.transform;
        public static GameObject   GameObject => Instance.gameObject;

        protected override void Awake() {
            base.Awake();

            _movement = GetComponent<Control>();
            _playerHealth = GetComponent<PlayerHealth>();
        }
    }
}
