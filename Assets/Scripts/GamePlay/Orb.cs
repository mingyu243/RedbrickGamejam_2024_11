using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private OrbHealth orbHealth;
    private Rigidbody2D rb;

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
    }
}
