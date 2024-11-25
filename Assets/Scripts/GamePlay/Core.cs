using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private CoreHealth coreHealth;
    private Rigidbody2D rb;

    [SerializeField] private CoreLink _coreLinkRed;
    [SerializeField] private CoreLink _coreLinkYellow;
    [SerializeField] private CoreLink _coreLinkBlue;

    public Rigidbody2D Rb { get => rb; set => rb = value; }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        coreHealth = GetComponent<CoreHealth>();
    }

    public void InitState()
    {
        CoreData data =  Managers.Data.CoreDatas[0];

        coreHealth.SetMaxHealth(data.Hp);
        coreHealth.SetHealth(data.Hp);

        coreHealth.Init();
    }

    public void SetLinkPower(int stayingZoneIndex)
    {
        int zoneCount = Managers.GamePlay.MainGame.CoreEffectController.ZoneCount;
        float ratio = (float)stayingZoneIndex / (float)zoneCount;

        _coreLinkRed.SetPower(1 - ratio);
        _coreLinkYellow.SetPower(1 - ratio);
        _coreLinkBlue.SetPower(1 - ratio);
    }

    public void Die()
    {
        coreHealth.Die();
    }
}
