using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] MainGame _mainGame;
    public MainGame MainGame => _mainGame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainGame.GameState == GameState.Battle)
            {
                Managers.Ui.ToggleSettingPopup();
            }
        }
    }
}
