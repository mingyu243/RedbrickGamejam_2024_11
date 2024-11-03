using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] int _stayingZoneIndex;

    [SerializeField] Zone[] _zones;

    public Zone[] Zones => _zones;

    public void Init()
    {
        _stayingZoneIndex = -1;
    }

    public Vector3 GetRandomPosition(int zoneIndex)
    {
        return Zones[zoneIndex].GetRandomPosition();
    }

    public void OnTriggerStayPlayer(int zoneIndex)
    {
        if (_stayingZoneIndex == -1)
        {
            _stayingZoneIndex = zoneIndex;
            EffectPlayer();
        }
    }

    public void OnTriggerExitPlayer(int zoneIndex)
    {
        if (_stayingZoneIndex == zoneIndex)
        {
            _stayingZoneIndex = -1;
        }
    }

    public void EffectPlayer()
    {
        ZoneData zoneData = Managers.Data.ZoneDatas[_stayingZoneIndex];

        Player player = Managers.GamePlay.MainGame.Player;
        player.Weapon.SetWeaponProperties(zoneData.WeaponCount, zoneData.RotationSpeed);
        player.PlayerMental.ChangeRate = zoneData.PlayerMentalChangeRate;

        Orb orb = Managers.GamePlay.MainGame.Orb;
        orb.SetLinkPower(_stayingZoneIndex);
    }
}
