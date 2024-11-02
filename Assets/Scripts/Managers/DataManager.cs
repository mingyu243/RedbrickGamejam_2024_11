using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    const string DOC_ID = "10NiacqcUdul9EiHZGjPd85O67-KLl42qpaVduHxH9GA";

    const string PLAYER_DATA_G_ID = "1860695544";
    const string MONSTER_DATA_G_ID = "1672973443";
    const string ORB_DATA_G_ID = "320756229";
    const string TIME_EVENT_DATA_G_ID = "72613725";
    const string WAVE_DATA_G_ID = "1412932090";
    const string ZONE_DATA_G_ID = "1239032604";

    // 스프레드 시트에서 읽어온 데이터에서 윗 행을 버림
    // 1,2번째 행은 설명 적는 위치, 3번째 행은 변수 있는 위치
    const int DUMMY_COUNT = 3;

    [SerializeField] PlayerData[] _playerDatas;
    [SerializeField] MonsterData[] _monsterDatas;
    [SerializeField] OrbData[] _orbDatas;
    [SerializeField] TimeEventData[] _timeEventDatas;
    [SerializeField] WaveData[] _waveDatas;
    [SerializeField] ZoneData[] _zoneDatas;

    public PlayerData[] PlayerDatas => _playerDatas;
    public MonsterData[] MonsterDatas => _monsterDatas;
    public OrbData[] OrbDatas => _orbDatas;
    public TimeEventData[] TimeEventDatas => _timeEventDatas;
    public WaveData[] TaveDatas => _waveDatas;
    public ZoneData[] ZoneDatas => _zoneDatas;

    public IEnumerator Init()
    {
        yield return GoogleSheetsLoader.LoadData(DOC_ID, PLAYER_DATA_G_ID, ParseCSVDataPlayerData);
        yield return GoogleSheetsLoader.LoadData(DOC_ID, MONSTER_DATA_G_ID, ParseCSVDataMonsterData);
        /**yield return GoogleSheetsLoader.LoadData(DOC_ID, ORB_DATA_G_ID, ParseCSVDataOrbData);
        yield return GoogleSheetsLoader.LoadData(DOC_ID, TIME_EVENT_DATA_G_ID, ParseCSVDataTimeEventData);
        yield return GoogleSheetsLoader.LoadData(DOC_ID, WAVE_DATA_G_ID, ParseCSVDataWaveData);
        yield return GoogleSheetsLoader.LoadData(DOC_ID, ZONE_DATA_G_ID, ParseCSVDataZoneData);**/
    }

    private void ParseCSVDataPlayerData(string csvData)
    {
        string[] rows = csvData.Split('\n');  // 각 줄을 분리하여 배열로 저장
        int rowsCount = rows.Length - DUMMY_COUNT;

        _playerDatas = new PlayerData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];

            string[] values = row.Split(',');

            int attack = int.Parse(values[0]);
            float attackSpeed = float.Parse(values[1]);
            int hp = int.Parse(values[2]);
            float moveSpeed = float.Parse(values[3]);
            float attackRange = float.Parse(values[4]);

            PlayerData playerData = new PlayerData()
            {
                Attack = attack,
                AttackSpeed = attackSpeed,
                Hp = hp,
                MoveSpeed = moveSpeed,
                AttackRange = attackRange,
            };
            _playerDatas[i] = playerData;
        }
    }

    private void ParseCSVDataMonsterData(string csvData)
    {
        string[] rows = csvData.Split('\n');
        int rowsCount = rows.Length - DUMMY_COUNT;

        _monsterDatas = new MonsterData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];
            string[] values = row.Split(',');

            int id = int.Parse(values[0]);
            int attack = int.Parse(values[1]);
            float attackSpeed = float.Parse(values[2]);
            int hp = int.Parse(values[3]);
            float moveSpeed = float.Parse(values[4]);

            MonsterData monsterData = new MonsterData()
            {
                Id = id,
                Attack = attack,
                AttackSpeed = attackSpeed,
                Hp = hp,
                MoveSpeed = moveSpeed
            };
            _monsterDatas[i] = monsterData;
        }
    }

    private void ParseCSVDataOrbData(string csvData)
    {
        string[] rows = csvData.Split('\n');
        int rowsCount = rows.Length - DUMMY_COUNT;

        _orbDatas = new OrbData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];
            string[] values = row.Split(',');

            int hp = int.Parse(values[0]);

            OrbData orbData = new OrbData()
            {
                Hp = hp,
            };
            _orbDatas[i] = orbData;
        }
    }

    private void ParseCSVDataTimeEventData(string csvData)
    {
        string[] rows = csvData.Split('\n');
        int rowsCount = rows.Length - DUMMY_COUNT;

        _timeEventDatas = new TimeEventData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];
            string[] values = row.Split(',');

            int eventTime = int.Parse(values[0]);
            int waveId = int.Parse(values[1]);

            TimeEventData timeEventData = new TimeEventData()
            {
                EventTime = eventTime,
                WaveId = waveId,
            };
            _timeEventDatas[i] = timeEventData;
        }
    }

    private void ParseCSVDataWaveData(string csvData)
    {
        string[] rows = csvData.Split('\n');
        int rowsCount = rows.Length - DUMMY_COUNT;

        _waveDatas = new WaveData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];
            string[] values = row.Split(',');

            int id = int.Parse(values[0]);
            int monster1Weight = int.Parse(values[1]);
            int monster2Weight = int.Parse(values[2]);
            int monster3Weight = int.Parse(values[3]);
            int spawnCount = int.Parse(values[4]);

            WaveData data = new WaveData()
            {
                Id = id,
                Monster1Weight = monster1Weight,
                Monster2Weight = monster2Weight,
                Monster3Weight = monster3Weight,
                SpawnCount = spawnCount
            };
            _waveDatas[i] = data;
        }
    }

    private void ParseCSVDataZoneData(string csvData)
    {
        string[] rows = csvData.Split('\n');
        int rowsCount = rows.Length - DUMMY_COUNT;

        _zoneDatas = new ZoneData[rowsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            string row = rows[i + DUMMY_COUNT];
            string[] values = row.Split(',');

            int id = int.Parse(values[0]);
            int randMin = int.Parse(values[1]);
            int randMax = int.Parse(values[2]);
            float increasePlayerHpTime = float.Parse(values[3]);

            ZoneData data = new ZoneData()
            {
                Id = id,
                RandMin = randMin,
                RandMax = randMax,
                IncreasePlayerHpTime = increasePlayerHpTime
            };
            _zoneDatas[i] = data;
        }
    }
}
