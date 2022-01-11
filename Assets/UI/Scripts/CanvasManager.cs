using System;
using UnityEngine;

namespace UI {
    public class CanvasManager : Singleton<CanvasManager> {
        [SerializeField] private HealthUI _healthUI;
        [SerializeField] private DamageScreen _damageScreen;

        public static HealthUI HealthUI => Instance._healthUI;

        public static DamageScreen DamageScreen => Instance._damageScreen;
    }
}
