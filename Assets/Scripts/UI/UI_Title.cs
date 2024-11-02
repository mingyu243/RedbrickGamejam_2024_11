using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        Managers.GamePlay.MainGame.StartGame();
    }

    public void OnClickExitButton()
    {
        Managers.Application.Exit();
    }
}
