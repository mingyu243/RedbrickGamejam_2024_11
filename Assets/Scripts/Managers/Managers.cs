using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;

    void Awake()
    {
        _instance = this;
    }

    [SerializeField] bool _isInit;
    public static bool IsInit => _instance._isInit;

    [SerializeField] UIManager _ui;
    [SerializeField] ApplicationManager _application;
    [SerializeField] GamePlayManager _gamePlay;

    public static UIManager Ui => _instance._ui;
    public static ApplicationManager Application => _instance._application;
    public static GamePlayManager GamePlay => _instance._gamePlay;


    void Start()
    {
        _isInit = false;

        // 초기화

        _isInit = true;
    }
}
