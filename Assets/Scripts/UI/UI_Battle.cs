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
    [Space]
    [SerializeField] GameObject _waveAlert;
    [SerializeField] TMP_Text _waveAlertNumberText;
    [Space]
    [SerializeField] Button _testOrbDeathButton;
    [SerializeField] Button _testPlayerDeathButton;
    [SerializeField] Button _settingButton;

    void Start()
    {
        _testOrbDeathButton.onClick.AddListener(OnClickTestOrbDeath);
        _testPlayerDeathButton.onClick.AddListener(OnClickTestPlayerDeath);
        _settingButton.onClick.AddListener(OnClickSetting);
    }

    private void OnEnable()
    {
        _waveAlert.SetActive(false);
    }

    public void OnClickTestOrbDeath()
    {
        Managers.GamePlay.MainGame.GameResult = GameResult.OrbDeath;
    }

    public void OnClickTestPlayerDeath()
    {
        Managers.GamePlay.MainGame.GameResult = GameResult.PlayerDeath;
    }

    public void OnClickSetting()
    {
        Managers.Ui.SetVisibleSettingPopup(true);
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
