﻿using DG.Tweening;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameOverController : MonoBehaviour {
  [field: SerializeField]
  public PostProcessVolume CameraEffect { get; private set; }

  [field: SerializeField, Header("HighestCombo")]
  public TMPro.TMP_Text HighestComboLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text HighestComboValue { get; private set; }

  [field: SerializeField, Header("FinalScore")]
  public TMPro.TMP_Text FinalScoreLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text FinalScoreValue { get; private set; }

  private CanvasGroup _canvasGroup;

  public void Awake() {
    _canvasGroup = GetComponent<CanvasGroup>();
    _canvasGroup.alpha = 0f;
  }

  public void ShowGameOver(int finalScore, int highestCombo) {
    CameraEffect.enabled = true;

    DOTween.Kill(gameObject.GetInstanceID(), complete: true);

    DOTween.Sequence()
        .SetLink(gameObject)
        .SetId(gameObject.GetInstanceID())
        .Insert(0f, DOTween.To(() => _canvasGroup.alpha, a => _canvasGroup.alpha = a, 1f, 0.5f))
        .Insert(0f, AnimateHighestCombo(highestCombo))
        .Insert(1f, AnimateFinalScore(finalScore));
  }

  public Sequence AnimateHighestCombo(int highestCombo) {
    return DOTween.Sequence()
        .SetLink(gameObject)
        .SetId(gameObject.GetInstanceID())
        .Insert(0f, HighestComboLabel.DOFade(0f, 2f).From())
        .Insert(0f, HighestComboLabel.transform.DOLocalMoveX(15f, 1f).From(true))
        .Insert(0f, HighestComboValue.DOFade(0f, 2f).From())
        .Insert(0f, HighestComboValue.transform.DOLocalMoveX(-25f, 1f).From(true))
        .Insert(0f, HighestComboValue.DOCounter(0, highestCombo, 2f, false));
  }

  public Sequence AnimateFinalScore(int finalScore) {
    return DOTween.Sequence()
        .SetLink(gameObject)
        .SetId(gameObject.GetInstanceID())
        .Insert(0f, FinalScoreLabel.DOFade(0f, 2f).From())
        .Insert(0f, FinalScoreLabel.transform.DOLocalMoveX(15f, 1f).From(true))
        .Insert(0f, FinalScoreValue.DOFade(0f, 2f).From())
        .Insert(0f, FinalScoreValue.transform.DOLocalMoveX(-25f, 1f).From(true))
        .Insert(0f, FinalScoreValue.DOCounter(0, finalScore, 2f, false));
  }

  public void HideGameOver() {
    DOTween.Kill(gameObject.GetInstanceID(), complete: true);
    DOTween.Sequence()
        .SetLink(gameObject)
        .SetId(gameObject.GetInstanceID())
        .Insert(0f, DOTween.To(() => _canvasGroup.alpha, a => _canvasGroup.alpha = a, 0f, 0.5f));

    CameraEffect.enabled = false;
  }
}
