using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartAlertTextSetting : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    string baseDesc = "Zone {0} : 무기 개수 {1} 회전 속도 {2} 체력 회복 {3}\n";

    bool isInit = false;

    private void OnEnable()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;

        string desc = string.Empty;

        ZoneData[] zoneDatas = Managers.Data.ZoneDatas;
        for (int i = 0; i < zoneDatas.Length; i++)
        {
            ZoneData zoneData = zoneDatas[i];
            desc += string.Format(baseDesc, i + 1, zoneData.WeaponCount, zoneData.RotationSpeed, zoneData.PlayerMentalChangeRate);
        }

        _text.text = desc;
    }
}
