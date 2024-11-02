using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Battle : MonoBehaviour
{
    [SerializeField] TMP_Text _timeText;
    [Space]
    [SerializeField] Button _testOrbDeathButton;
    [SerializeField] Button _testPlayerDeathButton;

    void Start()
    {
        _testOrbDeathButton.onClick.AddListener(OnClickTestOrbDeath);
        _testPlayerDeathButton.onClick.AddListener(OnClickTestPlayerDeath);
    }

    public void OnClickTestOrbDeath()
    {
        Managers.GamePlay.SetResult(GameResult.OrbDeath);
    }

    public void OnClickTestPlayerDeath()
    {
        Managers.GamePlay.SetResult(GameResult.PlayerDeath);
    }

    public void SetTime(float time)
    {
        _timeText.text = time.ToString();
    }
}
