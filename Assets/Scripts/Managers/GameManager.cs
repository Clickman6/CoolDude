using System;
using UnityEngine;
using UnityEngine.Events;

namespace Managers {
    public class GameManager : Singleton<GameManager> {
        public static bool IsPause;

        [SerializeField] private UnityEvent OnGamePause;
        [SerializeField] private UnityEvent OnGameUnPause;
        [SerializeField] private UnityEvent OnGameLosing;
        [SerializeField] private UnityEvent OnGameWinning;
        
        protected override void Awake() {
            base.Awake();
            UnPause();
        }

        public void GameIsOver() {
            if (EnemyController.Instance._enemies.Count <= 0) {
                Winning();
            } else {
                Losing();
            }
        }

        public void Losing() {
            OnGameLosing.Invoke();
            Pause();
        }

        public void Winning() {
            OnGameWinning.Invoke();
            Pause();
        }

        public static void Pause() {
            Instance.OnGamePause.Invoke();

            IsPause = true;
            Time.timeScale = 0;
        }

        public static void UnPause() {
            Instance.OnGameUnPause.Invoke();

            IsPause = false;
            Time.timeScale = 1;
        }
    }
}
