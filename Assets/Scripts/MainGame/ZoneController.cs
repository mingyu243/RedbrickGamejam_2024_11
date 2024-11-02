using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] int _stayingZoneIndex;

    public void Init()
    {
        _stayingZoneIndex = -1;
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
        Player player = Managers.GamePlay.MainGame.Player;

        ZoneData zoneData = Managers.Data.ZoneDatas[_stayingZoneIndex];

        player.Weapon.SetWeaponProperties(zoneData.WeaponCount, zoneData.RotationSpeed);
    }
}
