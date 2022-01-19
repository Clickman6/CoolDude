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
            Color defaultColor = new Color();
            Color color = new Color();

            for (float t = 0f; t < 1f; t += Time.deltaTime) {
                color.r = (float)(Mathf.Sin(t * 15) * 0.5 + 0.5);

                for (var i = 0; i < _renderers.Length; i++) {
                    for (var j = 0; j < _renderers[i].materials.Length; j++) {
                        _renderers[i].materials[j].SetColor(EmissionColor, color);
                    }
                }

                yield return null;
            }

            for (var i = 0; i < _renderers.Length; i++) {
                for (var j = 0; j < _renderers[i].materials.Length; j++) {
                    _renderers[i].materials[j].SetColor(EmissionColor, defaultColor);
                }
            }
        }
    }
}
