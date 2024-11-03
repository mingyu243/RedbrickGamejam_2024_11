using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HpPlusAnimation : MonoBehaviour
{
    [SerializeField] RectTransform _icon;
    [SerializeField] float moveDistance = 50f;      // 위로 올라갈 거리
    [SerializeField] float duration = 0.8f;         // 전체 애니메이션 지속 시간
    [SerializeField] float popScale = 1.3f;         // 팝 효과 시 스케일 크기

    Sequence plusSequence;

    private void OnEnable()
    {
        if (plusSequence != null)
        {
            plusSequence.Kill();
        }

        // 초기 위치 및 투명도 설정
        _icon.anchoredPosition = Vector2.zero;
        Image iconImage = _icon.GetComponent<Image>();
        Color initialColor = iconImage.color;
        initialColor.a = 1;
        iconImage.color = initialColor;

        plusSequence = DOTween.Sequence();

        // Plus 애니메이션 시작
        plusSequence.Append(_icon.DOScale(popScale, 0.2f).SetEase(Ease.OutBack))           // 팝 효과
                    .Append(_icon.DOScale(1f, 0.15f).SetEase(Ease.OutBack))                // 원래 크기로 복귀
                    .Append(_icon.DOAnchorPos(new Vector2(0, moveDistance), duration).SetEase(Ease.OutCubic)) // 위로 이동
                    .Join(iconImage.DOFade(0, duration).SetEase(Ease.InCubic))             // Fade Out
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });
    }
}