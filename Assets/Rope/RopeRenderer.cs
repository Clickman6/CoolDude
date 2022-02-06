using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour {
    private LineRenderer _lineRenderer;

    [SerializeField] private int _segments = 10;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Draw(Vector3 start, Vector3 end, float length) {
        _lineRenderer.enabled = true;

        float interpolate = Vector3.Distance(start, end) / length;
        float offset = Mathf.Lerp(length / 2f, 0f, interpolate);

        Vector3 a = start + Vector3.down * offset;
        Vector3 b = end + Vector3.down * offset;

        _lineRenderer.positionCount = _segments + 1;

        for (int i = 0; i < _lineRenderer.positionCount; i++) {
            _lineRenderer.SetPosition(i, Bezier.GetPoint(start, a, b, end, (float)i / _segments));
        }
    }

    public void Hide() {
        _lineRenderer.enabled = false;
    }
}
