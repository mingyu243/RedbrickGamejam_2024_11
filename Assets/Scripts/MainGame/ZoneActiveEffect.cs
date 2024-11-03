using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ZoneActiveEffect : MonoBehaviour
{
    [SerializeField] int _zoneId;

    [SerializeField] TMP_Text _descriptText1;
    [SerializeField] TMP_Text _descriptText2;

    IEnumerator Start()
    {
        _descriptText1.text = string.Empty;
        _descriptText2.text = string.Empty;

        yield return new WaitUntil(() => Managers.IsInit);

        string baseDesc = "     x {0}\r\n회전 속도 {1}\r\n회복 속도 {2}";

        ZoneData zoneData = Managers.Data.ZoneDatas[_zoneId];

        string desc = string.Format(baseDesc, zoneData.WeaponCount, zoneData.RotationSpeed, zoneData.PlayerMentalChangeRate);

        _descriptText1.text = desc;
        _descriptText2.text = desc;
    }

    bool _isShow = true;

    [SerializeField] SpriteRenderer[] _spriteRenderers;
    [SerializeField] TMP_Text[] _texts;

    Color showColor = new Color(1, 1, 1, 1f);
    Color hideColor = new Color(1, 1, 1, 67 / 255f);

    public void Show()
    {
        if (_isShow)
        {
            return;
        }

        _isShow = true;

        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            renderer.color = showColor;
        }
        foreach (TMP_Text text in _texts)
        {
            text.color = showColor;
        }
    }

    public void Hide()
    {
        if (!_isShow)
        {
            return;
        }

        _isShow = false;

        foreach (SpriteRenderer renderer in _spriteRenderers)
        {
            renderer.color = hideColor;
        }
        foreach (TMP_Text text in _texts)
        {
            text.color = hideColor;
        }
    }
}
