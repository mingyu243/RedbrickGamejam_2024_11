using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : MonoBehaviour
{
    [SerializeField] GameObject _startingUI;
    [SerializeField] GameObject _loadingUI;
    [Space]
    [SerializeField] Button _startButton;
    [SerializeField] Button _exitButton;

    void Start()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void SetVisibleLoadingUI(bool isVisible)
    {
        _startingUI.SetActive(!isVisible);
        _loadingUI.SetActive(isVisible);
    }

    public void OnClickStartButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);

        Managers.GamePlay.MainGame.StartGame();
    }

    public void OnClickExitButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);

        Managers.Application.Exit();
    }
}
