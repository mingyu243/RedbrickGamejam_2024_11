using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbEffectController : MonoBehaviour
{
    [SerializeField] OrbEffectZoneController _zoneController;
    [Space]
    [SerializeField] Orb _orb;
    [SerializeField] Player _player;
    [Space]
    [SerializeField] bool _isOn;
    [Space]
    [SerializeField] int _currZoneIndex = -1;

    public int ZoneCount => _zoneController.ZoneCount;

    IEnumerator Start()
    {
        _isOn = false;
        yield return new WaitUntil(() => Managers.IsInit);

        _zoneController.InstantiateZones();
        _isOn = true;
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

        float distance = Vector3.Distance(_orb.transform.position, _player.transform.position);
        int currZoneIndex = _zoneController.GetZoneIndex(distance);
        if (currZoneIndex != _currZoneIndex)
        {
            _currZoneIndex = currZoneIndex;
            OrbEffectZone zone = _zoneController.GetZone(_currZoneIndex);
            
            if (zone != null)
            {
                zone.Effect(_player, _orb);
            }
        }
    }
}
