using System.Collections;
using UnityEngine;

namespace Common {
    public class DamageBlink : MonoBehaviour {
        private Coroutine _coroutine;

        [SerializeField] private Renderer[] _renderers;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        public void StartBlink() {
            if (_coroutine != null) {
                StopCoroutine(_coroutine);
            }

            StartCoroutine(BlinkEffect());
        }

        private IEnumerator BlinkEffect() {
            Color color = new Color();

            for (float t = 0f; t < 1f; t += Time.deltaTime) {
                color.r = (float)(Mathf.Sin(t * 15) * 0.5 + 0.5);

                foreach (var renderer in _renderers) {
                    foreach (var material in renderer.materials) {
                        material.SetColor(EmissionColor, color);
                    }
                }

                yield return null;
            }

            foreach (var renderer in _renderers) {
                foreach (var material in renderer.materials) {
                    material.SetColor(EmissionColor, new Color());
                }
            }
        }
    }
}
