using DG.Tweening;

using UnityEngine;

public class PopupController : MonoBehaviour {
  [field: SerializeField, Header("Canvas")]
  public Canvas ParentCanvas { get; private set; }

  [field: SerializeField, Header("Hit")]
  public TMPro.TMP_Text HitLabel { get; private set; }

  [field: SerializeField, Header("Miss")]
  public TMPro.TMP_Text MissLabel { get; private set; }

  public void PopupHit(Vector2 position) {
    GameObject popup = Instantiate(HitLabel.gameObject, position, Quaternion.identity, ParentCanvas.transform);
    PopupLabel(popup.GetComponent<TMPro.TMP_Text>());
  }

  public void PopupMiss(Vector2 position) {
    GameObject popup = Instantiate(MissLabel.gameObject, position, Quaternion.identity, ParentCanvas.transform);
    PopupLabel(popup.GetComponent<TMPro.TMP_Text>());
  }

  public void PopupLabel(TMPro.TMP_Text label) {
    DOTween.Sequence()
        .SetLink(label.gameObject)
        .Insert(0f, label.transform.DOPunchScale(new Vector3(1.01f, 1.01f, 1.01f), 1f, 5, 0))
        .Insert(0f, label.transform.DOMoveY(25f, 2f).SetRelative(true))
        .Insert(0.5f, label.DOFade(0f, 1.5f))
        .OnComplete(() => Destroy(label.gameObject));
  }
}