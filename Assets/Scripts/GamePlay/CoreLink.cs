using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLink : MonoBehaviour
{
    [SerializeField] Transform _targetTr;
    [SerializeField] LineRenderer _lineRenderer;

    public void SetPower(float ratio)
    {
        SetMaterialPower(ratio);
    }

    void SetMaterialPower(float ratio)
    {
        float min = 1f;
        float max = 8f;

        float value = Mathf.Lerp(min, max, 1 - ratio);

        _lineRenderer.material.SetFloat("_Power", value);
    }

    public void SetWidth(float ratio)
    {
        float min = 0.1f;
        float max = 1.0f;

        float value = Mathf.Lerp(min, max, ratio);

        _lineRenderer.startWidth = value;
        _lineRenderer.endWidth = value;
    }

    public void SetColor()
    {
    }

    void Update()
    {
        _lineRenderer.SetPosition(1, _targetTr.position);
    }
}
