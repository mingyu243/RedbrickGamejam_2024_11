using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private OrbHealth orbHealth;
    private Rigidbody2D rb;

    [SerializeField] private OrbLink _orbLinkRed;
    [SerializeField] private OrbLink _orbLinkYellow;
    [SerializeField] private OrbLink _orbLinkBlue;

    public Rigidbody2D Rb { get => rb; set => rb = value; }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        orbHealth = GetComponent<OrbHealth>();
    }

    public void InitState()
    {
        OrbData data =  Managers.Data.OrbDatas[0];

        orbHealth.SetMaxHealth(data.Hp);
        orbHealth.SetHealth(data.Hp);

        orbHealth.Init();
    }

    public void SetLinkPower(int stayingZoneIndex)
    {
        int zoneCount = Managers.GamePlay.MainGame.OrbEffectController.ZoneCount;
        float ratio = (float)stayingZoneIndex / (float)zoneCount;

        _orbLinkRed.SetPower(1 - ratio);
        _orbLinkYellow.SetPower(1 - ratio);
        _orbLinkBlue.SetPower(1 - ratio);
    }
}
