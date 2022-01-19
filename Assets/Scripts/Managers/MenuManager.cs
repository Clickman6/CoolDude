using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers {
    public class MenuManager : MonoBehaviour {
        [SerializeField] private UnityEvent OnOpenMenu;
        [SerializeField] private UnityEvent OnCloseMenu;

        public void OpenMenu() {
            OnOpenMenu.Invoke();
            GameManager.Pause();
        }

        public void CloseMenu() {
            OnCloseMenu.Invoke();
            GameManager.UnPause();
        }

        public void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
