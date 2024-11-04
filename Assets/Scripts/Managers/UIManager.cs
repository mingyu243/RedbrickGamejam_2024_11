using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Title,
    Battle,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Title _title;
    [SerializeField] UI_Battle _battle;
    [Space]
    [SerializeField] GameObject _setting;
    [SerializeField] GameObject _loading;

    public UI_Title Title => _title;
    public UI_Battle Battle => _battle;

    void Start()
    {
        Title.gameObject.SetActive(false);
        Battle.gameObject.SetActive(false);

        _setting.SetActive(false);
    }

    public void SetVisibleLoadingUI(bool isVisible)
    {
        _loading.SetActive(isVisible);
    }

    public void ShowUI(UIType uiType)
    {
        Title.gameObject.SetActive(false);
        Battle.gameObject.SetActive(false);

        switch (uiType)
        {
            case UIType.Title: Title.gameObject.SetActive(true); break;
            case UIType.Battle: Battle.gameObject.SetActive(true); break;
        }
    }

    public void SetVisibleSettingPopup(bool isVisible)
    {
        _setting.SetActive(isVisible);
    }

    public void ToggleSettingPopup()
    {
        _setting.SetActive(!_setting.activeSelf);
    }
}
