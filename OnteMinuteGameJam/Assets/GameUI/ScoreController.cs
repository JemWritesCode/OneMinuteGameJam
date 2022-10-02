using DG.Tweening;

using UnityEngine;

public class ScoreController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text ScoreLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text ScoreValue { get; private set; }

  public void SetScoreValue(int value) {
    DOTween.Kill(ScoreValue.GetInstanceID(), complete: true);
    ScoreValue.SetText($"{value:G0}");
  }

  public void LerpScoreValue(int startValue, int endValue, float duration, Color lerpColor) {
    DOTween.Kill(ScoreValue.GetInstanceID(), complete: true);

    DOTween.Sequence()
        .SetLink(ScoreValue.gameObject)
        .SetId(ScoreValue.GetInstanceID())
        .Insert(0f, ScoreValue.DOCounter(startValue, endValue, duration, false))
        .Insert(0f, ScoreValue.DOColor(lerpColor, duration / 2f).SetLoops(2, LoopType.Yoyo));
  }
}