using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVictoryUI : MonoBehaviour
{
    [SerializeField] Button _restartButton;

    void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
    }

    public void OnClickRestartButton()
    {
        Managers.GamePlay.MainGame.RestartGame();
        this.gameObject.SetActive(false);
    }
}
