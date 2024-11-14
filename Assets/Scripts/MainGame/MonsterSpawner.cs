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

    public void Spawn(MonsterType monsterType)
    {
        GameObject monsterPrefab = null;
        Vector3 spawnPos = Vector3.zero;

        switch (monsterType)
        {
            case MonsterType.Common:
                {
                    monsterPrefab = _monster1Prefab;
                    //spawnPos = Managers.GamePlay.MainGame.ZoneController.GetRandomPosition(UnityEngine.Random.Range(0, 7));
                }
                break;
            case MonsterType.Rare:
                {
                    monsterPrefab = _monster2Prefab;
                    //spawnPos = Managers.GamePlay.MainGame.ZoneController.GetRandomPosition(UnityEngine.Random.Range(2, 7));
                }
                break;
            case MonsterType.Boss:
                {
                    monsterPrefab = _monster3Prefab;
                    //spawnPos = Managers.GamePlay.MainGame.ZoneController.GetRandomPosition(UnityEngine.Random.Range(4, 7));
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
