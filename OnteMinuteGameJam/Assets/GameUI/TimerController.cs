using System;

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

  private float _timerValue = 0f;

  public void StartTimer(float startValue, float endValue, TweenCallback onTimerComplete) {
    DOTween.Kill(TimerValue.GetInstanceID());

    _timerValue = startValue;
    DOTween.To(() => _timerValue, t => _timerValue = t, endValue, Mathf.Abs(endValue - startValue))
          .OnUpdate(() => SetTimerValue(_timerValue))
          .SetEase(Ease.Linear)
          .SetLink(TimerValue.gameObject)
          .SetId(TimerValue.GetInstanceID())
          .OnComplete(onTimerComplete);
  }

  private void SetTimerValue(float value) {
    TimerValue.SetText($"{value:00'<size=0%>'.'</size><sup>'00'</sup>'}");
  }

  public void StopTimer() {
    DOTween.Kill(TimerValue.GetInstanceID(), true);

    DOTween.Sequence()
        .SetLink(TimerValue.gameObject)
        .SetId(TimerValue.GetInstanceID())
        .Insert(0f, TimerValue.transform.DOShakePosition(1f, 10f));
  }
}