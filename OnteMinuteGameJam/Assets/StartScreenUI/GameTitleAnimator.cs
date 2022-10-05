using DG.Tweening;

using UnityEngine;

public class GameTitleAnimator : MonoBehaviour {
  [field: SerializeField]
  public TMPro.TMP_Text SmashingLabel { get; private set; }
  
  [field: SerializeField]
  public TMPro.TMP_Text PumpkinsLabel { get; private set; }

  public void Start() {
    DOTween.Sequence()
        .SetLink(gameObject)
        .Insert(0f, SmashingLabel.DOFade(0f, 2f).From())
        .Insert(0f, SmashingLabel.transform.DOLocalMoveX(-150f, 1f).SetEase(Ease.OutBack).From(true))
        .Insert(1.5f, SmashingLabel.transform.DOPunchPosition(new Vector3(50f, 0f, 0f), 10f, 1))
        .Insert(0.25f, PumpkinsLabel.DOFade(0f, 2f).From())
        .Insert(0.25f, PumpkinsLabel.transform.DOLocalMoveX(150f, 1f).SetEase(Ease.OutBack).From(true))
        .Insert(1.75f, PumpkinsLabel.transform.DOPunchPosition(new Vector3(-50f, 0f, 0f), 10f, 1));
  }
}