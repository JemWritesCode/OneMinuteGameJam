using DG.Tweening;

using UnityEngine;

public class TimerController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text TimerLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text TimerValue { get; private set; }

  public void AnimateIn() {
    DOTween.Kill(gameObject.GetInstanceID(), complete: true);

    DOTween.Sequence()
        .Insert(0f, TimerLabel.DOFade(0f, 2f).From().SetEase(Ease.Linear))
        .Insert(0f, TimerLabel.transform.DOLocalMoveX(25f, 2f).From(true))
        .Insert(0.5f, TimerValue.DOFade(0f, 2f).From().SetEase(Ease.Linear))
        .Insert(0.5f, TimerValue.transform.DOMoveX(25f, 2f).From(true))
        .SetLink(gameObject)
        .SetId(gameObject.GetInstanceID());
  }

  public void StartTimer(float startValue, float endValue) {
    DOTween.Kill(TimerValue.GetInstanceID());

    DOVirtual
        .Float(startValue, endValue, Mathf.Abs(endValue - startValue), v => SetTimerValue(v))
        .SetEase(Ease.Linear)
        .SetLink(TimerValue.gameObject)
        .SetId(TimerValue.GetInstanceID());
  }

  private void SetTimerValue(float value) {
    TimerValue.SetText($"{value:00'<size=0%>'.'</size><sup>'00'</sup>'}");
  }

  public void StopTimer() {
    DOTween.Kill(TimerValue.GetInstanceID());

    DOTween.Sequence()
        .SetLink(TimerValue.gameObject)
        .SetId(TimerValue.GetInstanceID())
        .Insert(0f, TimerValue.transform.DOShakePosition(1f, 10f));
  }
}