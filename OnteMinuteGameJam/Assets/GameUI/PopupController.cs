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

  [field: SerializeField]
  public GameObject PopupComboParent { get; private set; }

  public void PopupHit(Vector2 popupPosition, string hitText) {
    GameObject popup = Instantiate(HitLabel.gameObject, popupPosition, Quaternion.identity, ParentCanvas.transform);

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(hitText);

    DOTween.Sequence()
        .SetLink(popup)
        .Insert(0f, popup.transform.DOPunchScale(Vector3.one * 0.75f, 1f, 3, 0f))
        .Insert(0f, popup.transform.DOMoveY(25f, 2f).SetRelative(true))
        .Insert(0.5f, popupText.DOFade(0f, 1.5f))
        .OnComplete(() => Destroy(popup));
  }

  public void PopupMiss(Vector2 popupPosition, string missText) {
    GameObject popup = Instantiate(MissLabel.gameObject, popupPosition, Quaternion.identity, ParentCanvas.transform);

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(missText);

    DOTween.Sequence()
        .SetLink(popup)
        .Insert(0f, popup.transform.DOPunchScale(Vector3.one * -0.25f, 1f, 3, 0f))
        .Insert(0f, popup.transform.DOMoveY(-25f, 2f).SetRelative(true))
        .Insert(0.5f, popupText.DOFade(0f, 1.5f))
        .OnComplete(() => Destroy(popup));
  }

  public void PopupCombo(Vector2 popupPosition, string comboText) {
    DOTween.Kill(ComboLabel.GetInstanceID(), complete: true);

    GameObject popup = Instantiate(ComboLabel.gameObject, PopupComboParent.transform);
    popup.transform.localPosition = popupPosition;

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(comboText);

    DOTween.Sequence()
        .SetLink(popup)
        .SetId(ComboLabel.GetInstanceID())
        .Insert(0f, popup.transform.DOMoveX(-25f, 0.75f).From(true))
        .Insert(0f, popupText.DOFade(0f, 0.15f).From())
        .Insert(0.85f, popup.transform.DOMoveX(25f, 0.5f).SetRelative(true))
        .Insert(0.85f, popupText.DOFade(0f, 0.25f))
        .OnComplete(() => Destroy(popup));
  }

  public void PopupComboBroken(Vector2 popupPosition, string brokenText) {
    DOTween.Kill(ComboLabel.GetInstanceID(), complete: true);

    GameObject popup = Instantiate(ComboLabel.gameObject, PopupComboParent.transform);
    popup.transform.localPosition = popupPosition;

    TMPro.TMP_Text popupText = popup.GetComponent<TMPro.TMP_Text>();
    popupText.SetText(brokenText);

    DOTween.Sequence()
        .SetLink(popup)
        .SetId(ComboLabel.GetInstanceID())
        .Insert(0f, popup.transform.DOMoveX(-25f, 0.75f).From(true))
        .Insert(0f, popupText.DOFade(0f, 0.15f).From())
        .Insert(0.25f, popupText.DOColor(Color.red, 1.5f).SetEase(Ease.InOutFlash, 8, 0f))
        .Insert(1.75f, popupText.DOFade(0f, 0.25f))
        .OnComplete(() => Destroy(popup));
  }
}