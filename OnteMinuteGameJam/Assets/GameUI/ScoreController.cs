using DG.Tweening;

using UnityEngine;

public class ScoreController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text ScoreLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text ScoreValue { get; private set; }

  public void SetScoreValue(int value) {
    DOTween.Kill(ScoreValue.GetInstanceID());
    ScoreValue.SetText($"{value:G0}");
  }

  public void LerpScoreValue(int startValue, int endValue, float duration) {
    DOTween.Kill(ScoreValue.GetInstanceID());

    DOVirtual
        .Int(startValue, endValue, duration, v => ScoreValue.SetText($"{v:G0}"))
        .SetLink(ScoreValue.gameObject)
        .SetId(ScoreValue.GetInstanceID());
  }
}