using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDefeatUI : MonoBehaviour
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
        Managers.GamePlay.MainGame.RestartGame();
        this.gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Managers.Application.Exit();
    }
}
