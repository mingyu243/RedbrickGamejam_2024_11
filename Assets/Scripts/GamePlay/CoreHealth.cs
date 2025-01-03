using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealth : MonoBehaviour
{
    [SerializeField] GameObject _blue; // 60 ~ 100%
    [SerializeField] GameObject _yellow; // 30 ~ 60%
    [SerializeField] GameObject _red; // 0 ~ 30%
    [SerializeField] GameObject _destroy;
    [Space]
    public int maxHealth = 100; // 초기 체력
    public int health = 100; // 현재 체력

    [SerializeField] private CoreLink _coreLinkRed;
    [SerializeField] private CoreLink _coreLinkYellow;
    [SerializeField] private CoreLink _coreLinkBlue;
    [Space]
    [SerializeField] Slider _uiSlider;
    [SerializeField] TMP_Text _uiText;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _blue.SetActive(true);
        _yellow.SetActive(false);
        _red.SetActive(false);
        _destroy.SetActive(false);
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        _uiSlider.maxValue = value;
    }
    public void SetHealth(int value)
    {
        health = value;

        // UI 적용
        _uiSlider.value = value;
        _uiText.text = $"{health} / {maxHealth}";

        // 비율에 따른 모습 변화
        float ratio = (float)health / (float)maxHealth;
        _coreLinkRed.SetWidth(ratio);
        _coreLinkYellow.SetWidth(ratio);
        _coreLinkBlue.SetWidth(ratio);

        ratio *= 100;
        if (ratio >= 60)
        {
            _blue.SetActive(true);
            _yellow.SetActive(false);
            _red.SetActive(false);
        }
        else if (ratio >= 30)
        {
            _blue.SetActive(false);
            _yellow.SetActive(true);
            _red.SetActive(false);
        }
        else
        {
            _blue.SetActive(false);
            _yellow.SetActive(false);
            _red.SetActive(true);
        }
    }



    // 데미지를 받는 메서드
    public void TakeDamage(int damage)
    {
        // 게임이 끝났다면 데미지 입지 않음.
        if (Managers.GamePlay.MainGame.GameResult != GameResult.None)
        {
            return;
        }

        SetHealth(health - damage); // 데미지만큼 체력 감소
        AudioManager.instance.PlaySfx(AudioManager.Sfx.CoreHit);

        // 체력이 0 이하가 되면 파괴 처리
        if (health <= 0)
        {
            Managers.GamePlay.MainGame.GameResult = GameResult.CoreDeath;
        }
    }

    // 코어가 파괴되었을 때의 처리
    public void Die()
    {
        _blue.SetActive(false);
        _yellow.SetActive(false);
        _red.SetActive(false);
        _destroy.SetActive(true);
    }
}

