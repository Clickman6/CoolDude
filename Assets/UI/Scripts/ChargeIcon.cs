using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargeIcon : MonoBehaviour {
    [SerializeField] private Image _background;
    [SerializeField] private Image _foreground;
    [SerializeField] private TextMeshProUGUI _timer;

    public void StartCharge() {
        _background.color = ChangeColorAlpha(_background.color, 0.5f);

        _foreground.enabled = true;
        _timer.enabled = true;
    }

    public void StopCharge() {
        _background.color = ChangeColorAlpha(_background.color, 1f);

        _foreground.enabled = false;
        _timer.enabled = false;
    }

    public void ChangeCharge(float currentCharge, float maxCharge) {
        _foreground.fillAmount = 1 - currentCharge / maxCharge;
        _timer.text = Mathf.Ceil(maxCharge - currentCharge).ToString("0");
    }

    private Color ChangeColorAlpha(Color color, float alpha) {
        color.a = alpha;
        return color;
    }
}
