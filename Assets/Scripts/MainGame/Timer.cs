using UnityEngine.Events;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] bool _isOn;
    [SerializeField] float _elapsedTime;

    public float ElapsedTime => _elapsedTime;

    void Awake()
    {
        _isOn = false;
    }

    void Update()
    {
        if (!_isOn)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;
        Managers.Ui.Battle.SetTime(_elapsedTime);
    }

    public void ResetTimer()
    {
        _isOn = false;
        _elapsedTime = 0;
        
        Managers.Ui.Battle.SetTime(_elapsedTime);
    }

    public void Play()
    {
        _isOn = true;
    }

    public void Stop()
    {
        _isOn = false;
    }
}