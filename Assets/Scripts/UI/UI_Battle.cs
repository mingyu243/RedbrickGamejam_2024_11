using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Battle : MonoBehaviour
{
    [SerializeField] TMP_Text _timeText;
    [SerializeField] TMP_Text _waveNumberText;
    [SerializeField] TMP_Text _playerHpText;
    [Space]
    [SerializeField] GameObject _waveAlert;
    [SerializeField] TMP_Text _waveAlertNumberText;
    [Space]
    [SerializeField] Button _settingButton;

    void Start()
    {
        _settingButton.onClick.AddListener(OnClickSetting);
    }

    private void OnEnable()
    {
        _waveAlert.SetActive(false);
    }

    public void OnClickSetting()
    {
        Managers.Ui.SetVisibleSettingPopup(true);
    }

    public void SetPlayerHp(int hp)
    {
        _playerHpText.text = hp.ToString();
    }

    public void SetTime(float time)
    {
        _timeText.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }

    public void SetWaveNumber(int waveNumber)
    {
        _waveNumberText.text = waveNumber.ToString();
    }

    public void ShowWaveAlert(int waveNumber)
    {
        _waveAlertNumberText.text = waveNumber.ToString();
        _waveAlert.gameObject.SetActive(false);
        _waveAlert.gameObject.SetActive(true);
    }
}
