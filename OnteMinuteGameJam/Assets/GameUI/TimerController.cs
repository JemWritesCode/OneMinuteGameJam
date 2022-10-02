using DG.Tweening;

using UnityEngine;

public class TimerController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text TimerValue { private set; get; }

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