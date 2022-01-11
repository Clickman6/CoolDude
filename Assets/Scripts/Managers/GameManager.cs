using System;
using UnityEngine;

namespace Managers {
    public class GameManager : Singleton<GameManager> {
        public static bool IsPause;

        public static void Pause() {
            IsPause = true;
            Time.timeScale = 0;
        }

        public static void UnPause() {
            IsPause = false;
            Time.timeScale = 1;
        }
    }
}
