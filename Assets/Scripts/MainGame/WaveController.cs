using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] int _currentWaveId;
    public int CurrentWaveId { get => _currentWaveId; set => _currentWaveId = value; }

    public void Init()
    {
        _currentWaveId = 0;
    }

    public void StartWave(int waveId)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.WaveStart);
        Debug.Log($"웨이브 실행 {waveId}");
        CurrentWaveId = waveId;

        WaveData data = Managers.Data.WaveDatas[waveId];

        int i = 0;

        // 특정 라운드는 무조건 보스 몬스터가 한마리 나옴.
        if (waveId == 10 || waveId == 20 || waveId >= 30)
        {
            Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Boss);
            i++;
        }

        while (i < data.SpawnCount)
        {
            // TODO: 가중치 계산
            Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Common);
            i++;
        }
    }
}
