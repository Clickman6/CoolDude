using System.Collections.Generic;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : Singleton<EnemyController> {
    private Transform _target;

    public List<ActivateByDistance> _enemies = new List<ActivateByDistance>();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _enemiesLabel;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> OnEnemyCountChange;
    [SerializeField] private UnityEvent<float> OnAllEnemiesDie;

    protected override void Awake() {
        base.Awake();

        OnEnemyCountChange.AddListener(UpdateLabel);
    }

    private void Start() {
        _target = PlayerBase.Transform;
    }

    private void Update() {
        if (GameManager.IsPause) return;

        for (var i = 0; i < _enemies.Count; i++) {
            _enemies[i].CheckDistance(_target.position);
        }
    }

    public void AddEnemy(ActivateByDistance enemy) {
        _enemies.Add(enemy);

        OnEnemyCountChange.Invoke(_enemies.Count);
    }

    public void DeleteEnemy(ActivateByDistance enemy) {
        _enemies.Remove(enemy);

        OnEnemyCountChange.Invoke(_enemies.Count);

        if (_enemies.Count > 0) return;

        OnAllEnemiesDie.Invoke(Time.time);
    }

    private void UpdateLabel(int amount) {
        _enemiesLabel.text = amount.ToString();
    }
}
