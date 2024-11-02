using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // TODO: 가중치 계산
            Managers.GamePlay.MainGame.MonsterSpawner.Spawn(MonsterType.Common);
            i++;
        }
    }
}
