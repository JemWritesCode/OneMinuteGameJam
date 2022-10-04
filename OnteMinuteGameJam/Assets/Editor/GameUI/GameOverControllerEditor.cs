using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(GameOverController))]
public class GameOverControllerEditor : Editor {
  private GameOverController _gameOverController;

  public void OnEnable() {
    _gameOverController = Selection.activeGameObject.GetComponent<GameOverController>();
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);

    EditorGUILayout.Space(25f);
    ShowHideGameOverGUI();

    EditorGUI.EndDisabledGroup();
  }

  private void ShowHideGameOverGUI() {
    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("ShowGameOver")) {
      _gameOverController.ShowGameOver();
    }

    if (GUILayout.Button("HideGameOver")) {
      _gameOverController.HideGameOver();
    }

    EditorGUILayout.EndHorizontal();
  }
}
