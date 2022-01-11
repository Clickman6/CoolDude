using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class DamageScreen : MonoBehaviour {
        private Image _image;
        private Coroutine _coroutine;

        [SerializeField] private AnimationCurve _curve;

        private void Start() {
            _image = GetComponent<Image>();
        }

        public void StartEffect() {
            if (_coroutine != null) {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ShowEffect());
        }

        private IEnumerator ShowEffect() {
            Color color = _image.color;
            _image.enabled = true;

            for (float t = 0f; t < 1f; t += Time.deltaTime) {
                color.a = _curve.Evaluate(t);

                _image.color = color;

                yield return null;
            }

            _image.enabled = false;
        }
    }
}
