using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Title,
    Battle,
    End
}

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Title _title;
    [SerializeField] UI_Battle _battle;
    [SerializeField] UI_End _end;
    [Space]
    [SerializeField] GameObject _setting;

    public UI_Title Title => _title;
    public UI_Battle Battle => _battle;
    public UI_End End => _end;

    void Start()
    {
        Title.gameObject.SetActive(false);
        Battle.gameObject.SetActive(false);
        End.gameObject.SetActive(false);

        _setting.SetActive(false);
    }

    public void ShowUI(UIType uiType)
    {
        Title.gameObject.SetActive(false);
        Battle.gameObject.SetActive(false);
        End.gameObject.SetActive(false);

        switch (uiType)
        {
            case UIType.Title: Title.gameObject.SetActive(true); break;
            case UIType.Battle: Battle.gameObject.SetActive(true); break;
            case UIType.End: End.gameObject.SetActive(true); break;
        }
    }

    public void ToggleSettingPopup()
    {
        _setting.SetActive(!_setting.activeSelf);
    }
}
