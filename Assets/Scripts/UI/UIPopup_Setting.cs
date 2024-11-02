using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Setting : MonoBehaviour
{
    [SerializeField] TMP_Text _timeText;
    [Space]
    [SerializeField] Button _restartButton;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _exitButton;

    void OnEnable()
    {
        Time.timeScale = 0f;

        float time = Managers.GamePlay.MainGame.TimeController.CurrentTime;
        _timeText.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _resumeButton.onClick.AddListener(OnClickResumeButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        Managers.GamePlay.MainGame.RestartGame();
        this.gameObject.SetActive(false);
    }

    public void OnClickResumeButton()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Managers.Application.Exit();
    }
}
