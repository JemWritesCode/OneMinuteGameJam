using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class StartLabelAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [field: SerializeField]
  public Vector3 DoScaleEndValue { get; private set; } = Vector3.one;

  [field: SerializeField, Min(0f)]
  public float DoScaleDuration { get; private set; } = 0.5f;

  private Tweener _onHoverTweener;

  private void Start() {
    _onHoverTweener =
        transform
            .DOScale(DoScaleEndValue, DoScaleDuration)
            .SetLink(gameObject)
            .SetAutoKill(false)
            .Pause();

    DOTween.Sequence()
        .SetLink(gameObject)
        .Append(transform.DOLocalMoveY(10f, 1f).SetRelative(true).SetLoops(2, LoopType.Yoyo))
        .AppendInterval(1.5f)
        .SetLoops(-1, LoopType.Restart);

    GetComponent<TMPro.TMP_Text>()
        .DOFade(0f, 5f).From()
        .SetLink(gameObject);
  }


  public void OnPointerEnter(PointerEventData eventData) {
    _onHoverTweener.Restart();
  }

  public void OnPointerExit(PointerEventData eventData) {
    _onHoverTweener.SmoothRewind();
  }
}