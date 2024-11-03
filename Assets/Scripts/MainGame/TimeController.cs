using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] float _currentTime;
    [Space]
    [SerializeField] int _currTimeEventIndex;

    public float CurrentTime => _currentTime;

    public void Init()
    {
        _currTimeEventIndex = 0;

        _currentTime = 0;
        Managers.Ui.Battle.SetTime(0);
    }

    public void OnUpdate()
    {
        _currentTime += Time.deltaTime;
        Managers.Ui.Battle.SetTime(_currentTime);

        TimeEventData data = Managers.Data.TimeEventDatas[_currTimeEventIndex];
        if (data.EventTime <= _currentTime)
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
