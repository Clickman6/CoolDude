using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour {
    protected float _timer;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] protected float _shotPeriod;
    [SerializeField] private AudioSource _audioSource;

    [Header("Spawn")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _spawner;

    [Header("Flash & Smoke")]
    [SerializeField] private ParticleSystem _flash;
    [SerializeField] private ParticleSystem _smoke;

    protected virtual void Update() {
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
        Effects();
    }

    private void PlayShotSound() {
        _audioSource.pitch = Random.Range(0.75f, 1.25f);
        _audioSource.Play();
    }

    private void Effects() {
        _flash.Play();
        _smoke.Play();
    }

    public virtual void Activate() {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate() {
        gameObject.SetActive(false);
    }

    public virtual void PickUpLoot(int numberOfBullets) { }
}
