using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player {
    public class Gun : MonoBehaviour {
        private float _timer;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawner;
        [SerializeField] private GameObject _flash;
        [SerializeField] private float _speed;
        [SerializeField] private float _shotPeriod;

        private void Awake() {
            _flash.SetActive(false);
        }

        private void Update() {
            _timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Mouse0) && _timer > _shotPeriod) {
                CreateBullet();
            }
        }

        private void CreateBullet() {
            GameObject newBullet = Instantiate(_bulletPrefab, _spawner.position, _spawner.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = _spawner.forward * _speed;
            AudioManager.Instance.PlayShot(Random.Range(0.75f, 1.25f));

            _timer = 0;

            StopAllCoroutines();
            StartCoroutine(ShowFlash());
        }

        private IEnumerator ShowFlash() {
            _flash.SetActive(true);

            float scale = _flash.transform.localScale.x;
            scale = Random.Range(scale - 0.25f, scale + 0.25f);
            _flash.transform.localScale = Vector3.zero;
            _flash.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0, 90));

            while (_flash.transform.localScale.x != scale) {
                _flash.transform.localScale =
                    Vector3.MoveTowards(_flash.transform.localScale, Vector3.one * scale, Time.deltaTime * 100f);

                yield return null;
            }

            _flash.SetActive(false);
        }
    }
}
