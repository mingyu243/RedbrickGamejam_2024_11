using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] MainGame _mainGame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Ui.ToggleSettingPopup();
        }
    }

    public void StartGame()
    {
        _mainGame.StartGame();
    }

    public void ExitGame()
    {
        _mainGame.ExitGame();
    }

    public void SetResult(GameResult gameResult)
    {
        _mainGame.SetResult(gameResult);
    }
}
