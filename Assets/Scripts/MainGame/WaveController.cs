using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WaveController : MonoBehaviour
{
    [SerializeField] int _currentWaveId;
    public int CurrentWaveId => _currentWaveId;

    public void Init()
    {
        _currentWaveId = 0;
        Managers.Ui.Battle.SetWaveNumber(0);
    }

    public void StartWave(int waveId)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.WaveStart);
        Debug.Log($"웨이브 실행 {waveId}");
        _currentWaveId = waveId;

        Managers.Ui.Battle.SetWaveNumber(_currentWaveId + 1);
        Managers.Ui.Battle.ShowWaveAlert(_currentWaveId + 1);

        WaveData data = Managers.Data.WaveDatas[_currentWaveId];

        int i = 0;

        // 특정 라운드는 무조건 보스 몬스터가 한마리 나옴.
        if (_currentWaveId == 10 || _currentWaveId == 20 || _currentWaveId >= 30)
        {
            Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Boss);
            i++;
        }

        while (i < data.SpawnCount)
        {
            // 전체 가중치 합산
            float totalWeight = 0;
            totalWeight += data.Monster1Weight;
            totalWeight += data.Monster2Weight;
            totalWeight += data.Monster3Weight;

            float rand = Random.Range(0, totalWeight);

            // 누적 가중치
            float accumulatedWeight = 0;
            accumulatedWeight += data.Monster1Weight;
            if (rand < accumulatedWeight)
            {
                Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Common);
            }
            accumulatedWeight += data.Monster2Weight;
            if (rand < accumulatedWeight)
            {
                Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Rare);
            }
            accumulatedWeight += data.Monster3Weight;
            if (rand < accumulatedWeight)
            {
                Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Boss);
            }

            i++;
        }
    }
}
