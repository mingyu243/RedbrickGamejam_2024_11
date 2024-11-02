using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMental : MonoBehaviour
{
    bool _isOn;

    float _value;
    float _changeRate;

    [SerializeField] Slider _mentalSlider;

    private void Start()
    {
        Init();
    }

    public float ChangeRate { get => _changeRate; set => _changeRate = value; }
    public bool IsOn { get => _isOn; set => _isOn = value; }

    public void SetVisibleSlider(bool isVisible)
    {
        _mentalSlider.gameObject.SetActive(isVisible);
    }

    public void Init()
    {
        ResetValue();

        SetVisibleSlider(false);
        IsOn = false;
    }

    public void ResetValue()
    {
        _value = 50f;
        _mentalSlider.value = _value;
    }

    public void Update()
    {
        if (_isOn == false)
        {
            return;
        }

        _value += Time.deltaTime * ChangeRate;
        _mentalSlider.value = _value;

        if (_value <= 0)
        {
            ResetValue();
            Managers.GamePlay.MainGame.Player.PlayerHealth.TakeDamage(+1);
        }

        if (_value >= 100)
        {
            ResetValue();
            Managers.GamePlay.MainGame.Player.PlayerHealth.TakeDamage(-1);
        }
    }
}
