using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEffectZoneController : MonoBehaviour
{
    [SerializeField] GameObject _zonePrefab;

    // 생성된 Zone 배열
    CoreEffectZone[] _coreEffectZones;

    // 멀어지는 Zone일수록 알파 값이 어떻게 변할건지.
    const float ZONE_COLOR_ALPHA_MAX = 200f;
    const float ZONE_COLOR_ALPHA_MIN = 20f;

    // Zone 거리 데이터 백업 해놓음 (나중에 거리 계산 쉽게 하려고)
    List<(float dist, int index)> _zoneDistanceThresholds = new List<(float dist, int index)> ();

    public int ZoneCount => Managers.Data.CoreEffectDatas.Length;

    public void InstantiateZones()
    {
        CoreEffectData[] coreEffectDatas = Managers.Data.CoreEffectDatas;

        // Zone 생성
        _coreEffectZones = new CoreEffectZone[coreEffectDatas.Length];
        for (int i = 0; i < coreEffectDatas.Length; i++)
        {
            CoreEffectZone zone = Instantiate(_zonePrefab, this.transform).GetComponent<CoreEffectZone>();
            _coreEffectZones[i] = zone;
        }

        // Zone 셋업
        float totalMaxRadius = coreEffectDatas[coreEffectDatas.Length - 1].ZoneRadius;

        for (int i = 0; i < _coreEffectZones.Length; i++)
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
                minRadius = coreEffectDatas[i - 1].ZoneRadius;
            }
            maxRadius = coreEffectDatas[i].ZoneRadius;

            float alpha = Mathf.Lerp(ZONE_COLOR_ALPHA_MIN, ZONE_COLOR_ALPHA_MAX, 1 - ((float)i / (_coreEffectZones.Length - 1))); // 멀어지는 Zone일수록 알파값이 커지도록 함
            color = new Color(1, 1, 1, alpha / 255f);

            _coreEffectZones[i].SetUp(i, totalMaxRadius, minRadius, maxRadius, color);

            // 거리 백업
            _zoneDistanceThresholds.Add((maxRadius, i));
        }
    }

    public CoreEffectZone GetZone(int index)
    {
        try
        {
            return _coreEffectZones[index];
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
