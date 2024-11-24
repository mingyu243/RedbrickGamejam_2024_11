using System.Collections;
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
    [SerializeField] DataManager _data;
    [SerializeField] CameraManager _camera;
    [SerializeField] TimelineManager _timeline;

    public static UIManager Ui => _instance._ui;
    public static ApplicationManager Application => _instance._application;
    public static GamePlayManager GamePlay => _instance._gamePlay;
    public static DataManager Data => _instance._data;
    public static CameraManager Camera => _instance._camera;
    public static TimelineManager Timeline => _instance._timeline;

    IEnumerator Start()
    {
        _isInit = false;

        // 초기화
        yield return _data.Init();

        _isInit = true;
    }
}
