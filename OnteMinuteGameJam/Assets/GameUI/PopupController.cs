using DG.Tweening;

using UnityEditor;

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

[CustomEditor(typeof(PopupController))]
public class PopupControllerEditor : Editor {
  private PopupController _popupController;

  public void OnEnable() {
    _popupController = Selection.activeGameObject.GetComponent<PopupController>();
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);
    EditorGUILayout.Space(25f);
    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("PopupHit")) {
      _popupController.PopupHit(new Vector2(100f, 100f));
    }

    if (GUILayout.Button("PopupMiss")) {
      _popupController.PopupMiss(new Vector2(100f, 100f));
    }

    EditorGUILayout.EndHorizontal();
    EditorGUI.EndDisabledGroup();
  }
}
