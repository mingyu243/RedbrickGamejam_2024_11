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
    const float ZONE_COLOR_ALPHA_MAX = 0.1f;
    const float ZONE_COLOR_ALPHA_MIN = 0f;

    // Zone 거리 데이터 백업 해놓음 (나중에 거리 계산 쉽게 하려고)
    List<(float dist, int index)> _zoneDistanceThresholds = new List<(float dist, int index)> ();

    public int ZoneCount => Managers.Data.CoreEffectZoneDatas.Length;

    public void InstantiateZones()
    {
        CoreEffectZoneData[] datas = Managers.Data.CoreEffectZoneDatas;

        // Zone 생성
        _coreEffectZones = new CoreEffectZone[datas.Length];
        for (int i = 0; i < datas.Length; i++)
        {
            CoreEffectZone zone = Instantiate(_zonePrefab, this.transform).GetComponent<CoreEffectZone>();
            _coreEffectZones[i] = zone;
        }

        // Zone 셋업
        float totalMaxRadius = datas[datas.Length - 1].ZoneRadius;

        for (int i = 0; i < _coreEffectZones.Length; i++)
        {
            float minRadius;
            float maxRadius;

            if (i == 0)
            {
                minRadius = 0;
            }
            else
            {
                minRadius = datas[i - 1].ZoneRadius;
            }
            maxRadius = datas[i].ZoneRadius;

            float alpha = Mathf.Lerp(ZONE_COLOR_ALPHA_MIN, ZONE_COLOR_ALPHA_MAX, 1 - ((float)i / (_coreEffectZones.Length - 1))); // 멀어지는 Zone일수록 알파값이 커지도록 함

            _coreEffectZones[i].SetUp(i, totalMaxRadius, minRadius, maxRadius, alpha);

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

        return -1;
    }

    public Vector2 GetRandomPos(int minZoneId)
    {
        CoreEffectZoneData[] datas =  Managers.Data.CoreEffectZoneDatas;

        float minRadius = (minZoneId == 0) ? 0 : datas[minZoneId - 1].ZoneRadius;
        float maxRadius = datas[datas.Length - 1].ZoneRadius;

        // 랜덤 반지름 (minRadius와 maxRadius 사이)
        float radius = UnityEngine.Random.Range(minRadius, maxRadius);

        // 랜덤 각도 (0과 2π 사이)
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        // 극좌표를 직교좌표로 변환
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
