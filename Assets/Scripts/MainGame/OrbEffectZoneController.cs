using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbEffectZoneController : MonoBehaviour
{
    [SerializeField] Camera _minimapCamera; // Zone 크기에 맞춰서 미니맵 카메라 사이즈 조정
    [Space]
    [SerializeField] GameObject _zonePrefab;

    // 생성된 Zone 배열
    OrbEffectZone[] _orbEffectZones;

    // 멀어지는 Zone일수록 알파 값이 어떻게 변할건지.
    const float ZONE_COLOR_ALPHA_MAX = 200f;
    const float ZONE_COLOR_ALPHA_MIN = 20f;

    // Zone 거리 데이터 백업 해놓음 (나중에 거리 계산 쉽게 하려고)
    List<(float dist, int index)> _zoneDistanceThresholds = new List<(float dist, int index)> ();

    public int ZoneCount => Managers.Data.OrbEffectDatas.Length;

    public void InstantiateZones()
    {
        OrbEffectData[] orbEffectDatas = Managers.Data.OrbEffectDatas;

        // Zone 생성
        _orbEffectZones = new OrbEffectZone[orbEffectDatas.Length];
        for (int i = 0; i < orbEffectDatas.Length; i++)
        {
            OrbEffectZone zone = Instantiate(_zonePrefab, this.transform).GetComponent<OrbEffectZone>();
            _orbEffectZones[i] = zone;
        }

        // Zone 셋업
        float totalMaxRadius = orbEffectDatas[orbEffectDatas.Length - 1].ZoneRadius;

        for (int i = 0; i < _orbEffectZones.Length; i++)
        {
            float minRadius;
            float maxRadius;
            Color color;

            if (i == 0)
            {
                minRadius = 0;
            }
            else
            {
                minRadius = orbEffectDatas[i - 1].ZoneRadius;
            }
            maxRadius = orbEffectDatas[i].ZoneRadius;

            float alpha = Mathf.Lerp(ZONE_COLOR_ALPHA_MIN, ZONE_COLOR_ALPHA_MAX, 1 - ((float)i / (_orbEffectZones.Length - 1))); // 멀어지는 Zone일수록 알파값이 커지도록 함
            color = new Color(1, 1, 1, alpha / 255f);

            _orbEffectZones[i].SetUp(i, totalMaxRadius, minRadius, maxRadius, color);

            // 거리 백업
            _zoneDistanceThresholds.Add((maxRadius, i));
        }

        // 미니맵 카메라 셋업
        _minimapCamera.orthographicSize = totalMaxRadius;
    }

    public OrbEffectZone GetZone(int index)
    {
        try
        {
            return _orbEffectZones[index];
        }
        catch
        {
            return null;
        }
    }

    public int GetZoneIndex(float distance)
    {
        foreach (var item in _zoneDistanceThresholds)
        {
            if (item.dist >= distance)
            {
                return item.index;
            }
        }

        return - 1;
    }
}
