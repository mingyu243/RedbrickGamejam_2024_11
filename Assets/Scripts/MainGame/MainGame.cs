using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum GameState
{
    None,
    Init,
    Ready,
    Battle,
    Result,
    End
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
    [SerializeField] float _currentTime;
    [SerializeField] int _currentWaveId;
    [Space]
    [SerializeField] Player _player;

    public GameState GameState { get => _gameState; set => _gameState = value; }
    public GameResult GameResult { get => _gameResult; set => _gameResult = value; }
    public float CurrentTime { get => _currentTime; set => _currentTime = value; }
    public int CurrentWaveId { get => _currentWaveId; set => _currentWaveId = value; }

    void Awake()
    {
        _isPlaying = false;
        GameResult = GameResult.None;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        Managers.Ui.ShowUI(UIType.Title);
    }

    public void StartGame()
    {
        if (_isPlaying)
        {
            return;
        }

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
        SetResult(GameResult.Aborted);
    }

    public void SetResult(GameResult gameResult)
    {
        GameResult = gameResult;
    }

    public float GetCurrentTime()
    {
        return CurrentTime;
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
        CurrentTime = 0;
        Managers.Ui.Battle.SetTime(CurrentTime);

        // 초기화 로직.
        // 오브 초기화
        // 캐릭터 초기화
        _player.InitState();
        // 웨이브 발생 초기화

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
            CurrentTime += Time.deltaTime;
            Managers.Ui.Battle.SetTime(CurrentTime);

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
        yield return null;
    }
}
