using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour {
    private float _timer;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _shotPeriod;
    [SerializeField] private AudioSource _audioSource;

    [Header("Spawn")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _spawner;

    [Header("Flash")]
    [SerializeField] private Transform _flash;

    private Coroutine _flashCoroutine;

    private void Awake() {
        _flash.gameObject.SetActive(false);
    }

    protected void Update() {
        if (GameManager.IsPause) return;

        _timer += Time.unscaledDeltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && _timer > _shotPeriod) {
            _timer = 0;

            Shot();
        }
    }

    public virtual void Shot() {
        Rigidbody newBullet = Instantiate(_bulletPrefab, _spawner.position, _spawner.rotation);
        newBullet.velocity = _spawner.forward * _speed;

        PlayShotSound();
        StartFlash();
    }

    private void PlayShotSound() {
        _audioSource.pitch = Random.Range(0.75f, 1.25f);
        _audioSource.Play();
    }

    private void StartFlash() {
        if (_flashCoroutine != null) {
            StopCoroutine(_flashCoroutine);
        }

        _flashCoroutine = StartCoroutine(ShowFlash());
    }

    private IEnumerator ShowFlash() {
        _flash.gameObject.SetActive(true);

        float scale = _flash.localScale.x;
        scale = Random.Range(scale - 0.25f, scale + 0.25f);
        _flash.localScale = Vector3.zero;
        _flash.localEulerAngles = new Vector3(0f, 0f, Random.Range(0, 90));

        while (_flash.localScale.x != scale) {
            _flash.localScale =
                Vector3.MoveTowards(_flash.localScale, Vector3.one * scale, Time.deltaTime * 100f);

            yield return null;
        }

        _flash.gameObject.SetActive(false);
    }

    public virtual void Activate() {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate() {
        gameObject.SetActive(false);
    }

    public virtual void PickUpLoot(int numberOfBullets) { }
}
