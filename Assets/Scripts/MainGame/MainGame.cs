using System.Collections;
using UnityEngine;

public enum GameState
{
    None,
    Init,
    Ready,
    Battle,
    Result,
    End,
    Clear,
}

public enum GameResult
{
    None,
    OrbDeath,
    PlayerDeath,
    Aborted,
}

public class MainGame : MonoBehaviour
{
    [SerializeField] bool _isPlaying;
    [Space]
    [SerializeField] GameState _gameState;
    [SerializeField] GameResult _gameResult;
    [Space]
    [SerializeField] Player _player;
    [SerializeField] Orb _orb;
    [Space]
    [SerializeField] TimeController _timeController;
    [SerializeField] WaveController _waveController;
    [SerializeField] ZoneController _zoneController;
    [Space]
    [SerializeField] MonsterSpawner _monsterSpawner;
    [SerializeField] Transform _playerSpawnPositionTr;

    public GameState GameState { get => _gameState; set => _gameState = value; }
    public GameResult GameResult { get => _gameResult; set => _gameResult = value; }
    public TimeController TimeController => _timeController;
    public WaveController WaveController => _waveController;
    public ZoneController ZoneController => _zoneController;
    public MonsterSpawner MonsterSpawner => _monsterSpawner;
    public Player Player => _player;
    public Orb Orb => _orb;

    void Awake()
    {
        _isPlaying = false;
        GameResult = GameResult.None;
    }

    IEnumerator Start()
    {
        Player.transform.position = _playerSpawnPositionTr.position;

        yield return new WaitUntil(() => Managers.IsInit);

        Managers.Ui.ShowUI(UIType.Title);
    }

    public void StartGame()
    {
        if (_isPlaying)
        {
            return;
        }

        AudioManager.instance.PlayBgm(true);

        StartCoroutine(GameFlow());
    }

    public void RestartGame()
    {
        StartCoroutine(DoRestart());

        IEnumerator DoRestart()
        {
            // 기존 게임 끝내기
            GameResult = GameResult.Aborted;
            yield return new WaitUntil(() => (_isPlaying == false));

            // 게임 시작
            StartGame();
        }
    }

    public void ExitGame()
    {
        AudioManager.instance.PlayBgm(false);
        GameResult = GameResult.Aborted;
    }

    IEnumerator GameFlow()
    {
        _isPlaying = true;

        yield return InitPhase();
        yield return ReadyPhase();
        yield return BattlePhase();
        yield return ResultPhase();
        yield return EndPhase();

        _isPlaying = false;
    }

    IEnumerator InitPhase()
    {
        GameState = GameState.Init;

        GameResult = GameResult.None;

        TimeController.Init();
        WaveController.Init();
        ZoneController.Init();

        Player.InitState();
        Player.transform.position = _playerSpawnPositionTr.position;
        Orb.InitState();

        yield return null;
    }

    IEnumerator ReadyPhase()
    {
        GameState = GameState.Ready;

        Managers.Ui.ShowUI(UIType.Battle);
        yield return null;
    }

    IEnumerator BattlePhase()
    {
        GameState = GameState.Battle;

        while (GameResult == GameResult.None)
        {
            TimeController.OnUpdate();
            yield return null;
        }
    }

    IEnumerator ResultPhase()
    {
        GameState = GameState.Result;

        // 배틀 결과 연출.
        switch (GameResult)
        {
            case GameResult.None: break;
            case GameResult.OrbDeath:
                {
                    Debug.Log("오브 죽음");
                    yield return new WaitForSeconds(2);
                }
                break;
            case GameResult.PlayerDeath:
                {
                    Debug.Log("플레이어 죽음");
                    yield return new WaitForSeconds(2);
                }
                break;
            case GameResult.Aborted: break;
        }

    }

    IEnumerator EndPhase()
    {
        GameState = GameState.End;

        if (GameResult != GameResult.Aborted)
        {
            Managers.Ui.ShowUI(UIType.End);
        }

        MonsterSpawner.Clear();

        yield return null;
    }
}
