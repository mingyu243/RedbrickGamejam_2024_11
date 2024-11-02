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
    [SerializeField] Transform _spawnPositionTr;
    [Space]
    
    HashSet<Enemy> _enemies = new HashSet<Enemy>();

    public void Spawn(MonsterType monsterType)
    {
        GameObject monsterPrefab = null;

        switch (monsterType)
        {
            case MonsterType.Common: monsterPrefab = _monster1Prefab; break;
            case MonsterType.Rare: monsterPrefab = _monster2Prefab; break;
            case MonsterType.Boss: monsterPrefab = _monster3Prefab; break;
        }

        Enemy enemy = Instantiate(monsterPrefab, _spawnPositionTr).GetComponent<Enemy>();
        enemy.InitState();

        _enemies.Add(enemy);
    }

    public void Clear()
    {
        foreach (Enemy enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
        _enemies.Clear();
    }
}
