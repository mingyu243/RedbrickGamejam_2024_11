using System.Collections;
using UnityEngine;

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
    [SerializeField] GameResult _gameResult;
    [Space]
    [SerializeField] Timer _timer;

    void Awake()
    {
        _isPlaying = false;
        _gameResult = GameResult.None;
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

    public void ExitGame()
    {
        SetResult(GameResult.Aborted);
    }

    public void SetResult(GameResult gameResult)
    {
        _gameResult = gameResult;
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
        Debug.Log("StartPhase");

        _gameResult = GameResult.None;
        _timer.ResetTimer();

        // 초기화 로직.
        // 오브 초기화
        // 캐릭터 초기화
        // 웨이브 발생 초기화

        yield return null;
    }

    IEnumerator ReadyPhase()
    {
        Debug.Log("ReadyPhase");
        Managers.Ui.ShowUI(UIType.Battle);
        yield return null;
    }

    IEnumerator BattlePhase()
    {
        Debug.Log("BattlePhase");

        _timer.Play();

        yield return new WaitUntil(() => _gameResult != GameResult.None);

        _timer.Stop();
    }

    IEnumerator ResultPhase()
    {
        Debug.Log("ResultPhase");

        // 배틀 결과 연출.
        switch (_gameResult)
        {
            case GameResult.None: break;
            case GameResult.OrbDeath: Debug.Log("오브 죽음"); break;
            case GameResult.PlayerDeath: Debug.Log("플레이어 죽음"); break;
            case GameResult.Aborted: break;
        }

        yield return new WaitForSeconds(2);
    }

    IEnumerator EndPhase()
    {
        Managers.Ui.ShowUI(UIType.End);
        yield return null;
    }
}
