using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(PopupController))]
public class PopupControllerEditor : Editor {
  private PopupController _popupController;

  public void OnEnable() {
    _popupController = Selection.activeGameObject.GetComponent<PopupController>();

    _popupPosition = _popupController.ParentCanvas ? _popupController.ParentCanvas.pixelRect.center : Vector2.zero;
    _comboCounter = 0;
  }

  private Vector2 _popupPosition = Vector2.zero;
  private int _comboCounter = 0;

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);
    EditorGUILayout.Space(25f);

    _popupPosition = EditorGUILayout.Vector2Field("PopupPosition", _popupPosition);

    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("PopupHit")) {
      _popupController.PopupHit(_popupPosition, "+500");
    }

    if (GUILayout.Button("PopupMiss")) {
      _popupController.PopupMiss(_popupPosition, "-300");
    }

    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space(25f);
    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("PopupCombo")) {
      _popupController.PopupCombo(Vector2.zero, $"{_comboCounter++}<sup>Combo</sup>");
    }

    if (GUILayout.Button("PopupComboBroken")) {
      _popupController.PopupComboBroken(Vector2.zero, $"{_comboCounter++}<sup>Broken!</sup>");
    }

    EditorGUILayout.EndHorizontal();
    EditorGUI.EndDisabledGroup();
  }
}