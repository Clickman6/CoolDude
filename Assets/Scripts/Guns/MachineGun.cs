using TMPro;
using UnityEngine;

public class MachineGun : Gun {
    [Header("Machine Gun Params")]
    [SerializeField] private int _numberOfBullets;
    [SerializeField] protected Weapons _weapons;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _bulletsLabel;

    public override void Shot() {
        base.Shot();
        _numberOfBullets -= 1;
        UpdateLabel();

        if (_numberOfBullets > 0) return;

        _weapons.TakeGunByIndex();
    }

    public override void PickUpLoot(int numberOfBullets) {
        _numberOfBullets += numberOfBullets;
        UpdateLabel();
    }

    public override void Activate() {
        _bulletsLabel.gameObject.SetActive(true);
        UpdateLabel();

        base.Activate();
    }

    public override void Deactivate() {
        _bulletsLabel.gameObject.SetActive(false);

        base.Deactivate();
    }

    private void UpdateLabel() {
        _bulletsLabel.text = $"Пули: {_numberOfBullets.ToString()}";
    }
}
