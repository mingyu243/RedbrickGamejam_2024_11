using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Common = 0,
    Rare = 1,
    Boss = 2,
}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject _monster1Prefab;
    [SerializeField] GameObject _monster2Prefab;
    [SerializeField] GameObject _monster3Prefab;
    [Space]
    
    HashSet<Enemy> _enemies = new HashSet<Enemy>();

    int monster1MinZoneId;
    int monster2MinZoneId;
    int monster3MinZoneId;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        monster1MinZoneId = Managers.Data.MonsterDatas[0].MinSpawnZoneId;
        monster2MinZoneId = Managers.Data.MonsterDatas[1].MinSpawnZoneId;
        monster3MinZoneId = Managers.Data.MonsterDatas[2].MinSpawnZoneId;
    }

    public void Spawn(MonsterType monsterType)
    {
        GameObject monsterPrefab = null;
        Vector3 spawnPos = Vector3.zero;

        switch (monsterType)
        {
            case MonsterType.Common:
                {
                    monsterPrefab = _monster1Prefab;
                    spawnPos = Managers.GamePlay.MainGame.CoreEffectController.GetRandomPos(monster1MinZoneId);
                }
                break;
            case MonsterType.Rare:
                {
                    monsterPrefab = _monster2Prefab;
                    spawnPos = Managers.GamePlay.MainGame.CoreEffectController.GetRandomPos(monster2MinZoneId);
                }
                break;
            case MonsterType.Boss:
                {
                    monsterPrefab = _monster3Prefab;
                    spawnPos = Managers.GamePlay.MainGame.CoreEffectController.GetRandomPos(monster3MinZoneId);
                }
                break;
        }

        Enemy enemy = Instantiate(monsterPrefab).GetComponent<Enemy>();
        enemy.transform.position = spawnPos;
        enemy.InitState();

        _enemies.Add(enemy);
    }

    public void Clear()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        _enemies.Clear();
    }
}
