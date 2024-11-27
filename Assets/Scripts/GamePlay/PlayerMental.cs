using UnityEngine;
using UnityEngine.UI;

public class PlayerMental : MonoBehaviour
{
    bool _isOn;

    float _value;
    float _changeRate;

    [SerializeField] Slider _mentalSlider;
    [SerializeField] Image _mentalSliderFill;
    [SerializeField] GameObject _hpPlusAnimationObj;
    [SerializeField] GameObject _hpMinusAnimationObj;

    private void Start()
    {
        Init();
    }

    public float ChangeRate { get => _changeRate; set => _changeRate = value; }
    public bool IsOn { get => _isOn; set => _isOn = value; }

    public void SetVisibleSlider(bool isVisible)
    {
        _mentalSlider.gameObject.SetActive(isVisible);
    }

    public void Init()
    {
        ResetValue();

        SetVisibleSlider(false);
        IsOn = false;

        _mentalSliderFill.color = plusColor;

        _hpPlusAnimationObj.SetActive(false);
        _hpMinusAnimationObj.SetActive(false);
    }

    public void ResetValue()
    {
        _value = 80f;
        _mentalSlider.value = _value;
    }

    Color plusColor = new Color(1, 1, 1);
    Color minusColor = new Color(127 / 255f, 0, 207 / 255f);

    public void Update()
    {
        if (_isOn == false)
        {
            return;
        }

        if (ChangeRate >= 0)
        {
            _mentalSliderFill.color = plusColor;
        }
        else
        {
            _mentalSliderFill.color = minusColor;
        }

        _value += Time.deltaTime * ChangeRate;

        if (_value <= 0)
        {
            Managers.GamePlay.MainGame.Player.PlayerHealth.Die();
        }

        _value = Mathf.Clamp(_value, 0, 100);
        _mentalSlider.value = _value;

        //if (_value <= 0)
        //{
        //    ResetValue();
        //    Managers.GamePlay.MainGame.Player.PlayerHealth.TakeDamage(+1);
        //    _hpMinusAnimationObj.SetActive(false);
        //    _hpMinusAnimationObj.SetActive(true);
        //}

        //if (_value >= 100)
        //{
        //    ResetValue();
        //    Managers.GamePlay.MainGame.Player.PlayerHealth.TakeDamage(-1);
        //    _hpPlusAnimationObj.SetActive(false);
        //    _hpPlusAnimationObj.SetActive(true);
        //}
    }
}
