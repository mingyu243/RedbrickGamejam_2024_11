using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HpMinusAnimation : MonoBehaviour
{
    [SerializeField] RectTransform _icon;
    [SerializeField] RectTransform _breakIcon;

    [SerializeField] float popScale = 1.3f;    // 팝 효과 시 스케일 크기
    [SerializeField] float duration = 0.6f;     // 애니메이션 지속 시간

    Sequence minusSequence;

    private void OnEnable()
    {
        if (minusSequence != null)
        {
            minusSequence.Kill();
        }

        // 초기화: 부서진 아이콘 비활성화
        _icon.gameObject.SetActive(true);
        _breakIcon.gameObject.SetActive(false);

        Image image = _breakIcon.GetComponent<Image>();
        image.color = Color.white;

        minusSequence = DOTween.Sequence();

        // 팝 효과 및 Fade Out
        minusSequence.Append(_icon.DOScale(popScale, 0.2f).SetEase(Ease.OutBack))   // 기본 아이콘 팝 효과
                     .Append(_icon.DOScale(1f, 0.15f).SetEase(Ease.OutBack))         // 원래 크기로 복귀
                     .AppendCallback(() =>
                     {
                         // 기본 아이콘 비활성화 및 부서진 아이콘 활성화
                         _icon.gameObject.SetActive(false);
                         _breakIcon.gameObject.SetActive(true);
                     })
                     .Append(_breakIcon.GetComponent<Image>().DOFade(0, duration).SetEase(Ease.InCubic)) // 부서진 아이콘 Fade Out
                     .OnComplete(() =>
                     {
                         gameObject.SetActive(false); // 애니메이션 완료 후 객체 비활성화
                     });
    }
}