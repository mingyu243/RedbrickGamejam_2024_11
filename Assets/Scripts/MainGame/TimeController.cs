using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] float _currentTime;
    [Space]
    [SerializeField] int _currTimeEventIndex;

    public float Duration => _duration;
    public float CurrentTime => _currentTime;
    public float RemainTime => _duration - _currentTime;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        Time.timeScale = Managers.Data.ConfigDatas[0].GameTimeSpeed;
    }

    public void Init()
    {
        _currTimeEventIndex = 0;

        _duration = Managers.Data.ConfigDatas[0].GameDuration;
        _currentTime = 0;

        Managers.Ui.Battle.SetTime(RemainTime);
        Managers.Ui.Battle.SetNextWaveTime(_duration - Managers.Data.TimeEventDatas[0].TriggerTime);
    }

    public void OnUpdate()
    {
        _currentTime += Time.deltaTime;

        if (RemainTime <= 0)
        {
            Managers.Ui.Battle.SetTime(0);
            Managers.GamePlay.MainGame.GameResult = GameResult.TimeEnd;
        }
        else
        {
            Managers.Ui.Battle.SetTime(RemainTime);

            TimeEventData data = Managers.Data.TimeEventDatas[_currTimeEventIndex];
            if (data.TriggerTime <= _currentTime)
            {
                TriggerEvent(data);
                _currTimeEventIndex++;
            }
        }
    }

    public void TriggerEvent(TimeEventData data)
    {
        Managers.GamePlay.MainGame.WaveController.StartWave(data.WaveId);
        Managers.GamePlay.MainGame.CoreEffectController.BlockZoneIds(data.BlockZoneIds);
    }
}
