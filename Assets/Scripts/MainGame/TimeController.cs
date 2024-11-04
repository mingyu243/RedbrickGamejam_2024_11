using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] float _currentTime;
    [Space]
    [SerializeField] int _currTimeEventIndex;

    public float CurrentTime => _currentTime;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        Time.timeScale = Managers.Data.PlayerDatas[0].GameTimeSpeed;
    }

    public void Init()
    {
        _currTimeEventIndex = 0;

        _currentTime = 0;
        Managers.Ui.Battle.SetTime(0);
        Managers.Ui.Battle.SetNextWaveTime(Managers.Data.TimeEventDatas[0].TriggerTime);
    }

    public void OnUpdate()
    {
        _currentTime += Time.deltaTime;
        Managers.Ui.Battle.SetTime(_currentTime);

        TimeEventData data = Managers.Data.TimeEventDatas[_currTimeEventIndex];
        if (data.TriggerTime <= _currentTime)
        {
            TriggerEvent(data);
            _currTimeEventIndex++;
        }
    }

    public void TriggerEvent(TimeEventData data)
    {
        Managers.GamePlay.MainGame.WaveController.StartWave(data.WaveId);
    }
}
