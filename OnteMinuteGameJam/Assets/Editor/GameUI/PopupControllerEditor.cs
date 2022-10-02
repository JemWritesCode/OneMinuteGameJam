using UnityEditor;

using UnityEngine;

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