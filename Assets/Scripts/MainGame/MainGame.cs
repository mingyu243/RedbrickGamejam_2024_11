using Cinemachine;
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
    CoreDeath,
    PlayerDeath,
    TimeEnd,
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
    [SerializeField] Core _core;
    [Space]
    [SerializeField] TimeController _timeController;
    [SerializeField] WaveController _waveController;
    [SerializeField] CoreEffectController _coreEffectController;
    [SerializeField] CameraController _cameraController;
    [SerializeField] MapController _mapController;
    [Space]
    [SerializeField] MonsterSpawner _monsterSpawner;
    [SerializeField] Transform _playerSpawnPositionTr;

    public GameState GameState { get => _gameState; set => _gameState = value; }
    public GameResult GameResult { get => _gameResult; set => _gameResult = value; }
    public TimeController TimeController => _timeController;
    public WaveController WaveController => _waveController;
    public CoreEffectController CoreEffectController => _coreEffectController;
    public CameraController CameraController => _cameraController;
    public MapController MapController => _mapController;
    public MonsterSpawner MonsterSpawner => _monsterSpawner;
    public Player Player => _player;
    public Core Core => _core;

    void Awake()
    {
        _isPlaying = false;
        GameResult = GameResult.None;
    }

    IEnumerator Start()
    {
        Player.transform.position = _playerSpawnPositionTr.position;

        Managers.Ui.Title.SetVisibleLoadingUI(true);
        yield return new WaitUntil(() => Managers.IsInit);
        Managers.Ui.Title.SetVisibleLoadingUI(false);

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
        CameraController.Init();

        Player.InitState();
        Player.transform.position = _playerSpawnPositionTr.position;
        Core.InitState();

        yield return null;
    }

    IEnumerator ReadyPhase()
    {
        GameState = GameState.Ready;

        Managers.Ui.ShowUI(UIType.Battle);
        
        Managers.Ui.Battle.ShowGameStartAlert();
        yield return new WaitForSeconds(8f);

        Player.PlayerMental.SetVisibleSlider(true);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BattlePhase()
    {
        GameState = GameState.Battle;

        Player.PlayerMental.IsOn = true;

        while (GameResult == GameResult.None)
        {
            TimeController.OnUpdate();
            yield return null;
        }

        Player.PlayerMental.IsOn = false;
    }

    IEnumerator ResultPhase()
    {
        GameState = GameState.Result;

        // 배틀 결과 연출.
        switch (GameResult)
        {
            case GameResult.None: break;
            case GameResult.CoreDeath: // 게임 패배
                {
                    Debug.Log("코어 죽음");
                    yield return ShowDefeat();
                }
                break;
            case GameResult.PlayerDeath: // 게임 패배
                {
                    Debug.Log("플레이어 죽음");
                    yield return ShowDefeat();
                }
                break;
            case GameResult.TimeEnd: // 게임 승리
                {
                    Debug.Log("시간 끝");
                    yield return ShowVictory();
                }
                break;
            case GameResult.Aborted: break;
        }

    }

    IEnumerator EndPhase()
    {
        GameState = GameState.End;

        MonsterSpawner.Clear();
        Player.PlayerMental.SetVisibleSlider(false);

        yield return null;
    }

    IEnumerator ShowDefeat()
    {
        CinemachineVirtualCamera vcamCore = Managers.Camera.VirtualCamCore;
        vcamCore.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        Core.Die();

        yield return new WaitForSeconds(1);

        Managers.Ui.Battle.ShowGameDefeat();
    }

    IEnumerator ShowVictory()
    {
        //CinemachineVirtualCamera vcam = Managers.Camera.VirtualCam;

        yield return null;
        Managers.Ui.Battle.ShowGameVictory();
    }
}
