using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreEffectController : MonoBehaviour
{
    [SerializeField] CoreEffectZoneController _zoneController;
    [Space]
    [SerializeField] Core _core;
    [SerializeField] Player _player;
    [Space]
    [SerializeField] bool _isOn;
    [Space]
    [SerializeField] CoreEffectZone _currZone;

    public int ZoneCount => _zoneController.ZoneCount;

    IEnumerator Start()
    {
        _isOn = false;
        yield return new WaitUntil(() => Managers.IsInit);

        _zoneController.InstantiateZones();
        _isOn = true;
    }

    public Vector2 GetRandomPos(int minZoneId)
    {
        return _zoneController.GetRandomPos(minZoneId);
    }

    public void SetOn(bool isOn)
    {
        _isOn = isOn; 
    }

    private void Update()
    {
        if (_isOn == false)
        {
            return;
        }

        float distance = Vector3.Distance(_core.transform.position, _player.transform.position);
        int currZoneIndex = _zoneController.GetZoneIndex(distance);
        CoreEffectZone currZone = _zoneController.GetZone(currZoneIndex);

        if (_currZoneIndex currZone != _currZoneIndex)
        {
            _currZoneIndex = currZoneIndex;
            CoreEffectZone zone = _zoneController.GetZone(_currZoneIndex);
            if (zone != null)
            {
                zone.Effect(_player, _core);
            }
        }
    }

    public void BlockZoneIds(int[] blockZoneIds)
    {
        // 모든 구역에 대해 확인
        for (int i = 0; i < ZoneCount; i++)
        {
            // blockZoneIds 배열에 현재 구역 i가 포함되어 있는지 확인
            if (System.Array.Exists(blockZoneIds, zoneId => zoneId == i))
            {
                _zoneController.GetZone(i).Block();
            }
            else
            {
                _zoneController.GetZone(i).UnBlock();
            }
        }
    }
}
