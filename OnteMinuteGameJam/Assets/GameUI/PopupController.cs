using DG.Tweening;

using UnityEngine;

public class PopupController : MonoBehaviour {
  [field: SerializeField, Header("Canvas")]
  public Canvas ParentCanvas { get; private set; }

  [field: SerializeField, Header("Hit")]
  public TMPro.TMP_Text HitLabel { get; private set; }

  [field: SerializeField, Header("Miss")]
  public TMPro.TMP_Text MissLabel { get; private set; }

  [field: SerializeField, Header("Combo")]
  public TMPro.TMP_Text ComboLabel { get; private set; }

  public void PopupHit(Vector2 popupPosition, string hitText) {
    GameObject popup = Instantiate(HitLabel.gameObject, popupPosition, Quaternion.identity, ParentCanvas.transform);
    popup.SetActive(true);

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(hitText);

    PopupLabel(popupText);
  }

  public void PopupMiss(Vector2 popupPosition, string missText) {
    GameObject popup = Instantiate(MissLabel.gameObject, popupPosition, Quaternion.identity, ParentCanvas.transform);
    popup.SetActive(true);

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(missText);

    PopupLabel(popupText);
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