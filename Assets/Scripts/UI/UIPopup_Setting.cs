using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Setting : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        Managers.GamePlay.StartGame();
    }

    public void OnClickExitButton()
    {
        Managers.Application.Exit();
    }
}
