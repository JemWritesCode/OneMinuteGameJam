using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {
  private GameManager _gameManager;

  public void OnEnable() {
    _gameManager = Selection.activeGameObject.GetComponent<GameManager>();
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);
    EditorGUILayout.Space(25f);

    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("StartNewGame")) {
      _gameManager.StartNewGame();
    }

    if (GUILayout.Button("StopCurrentGame")) {
      _gameManager.StopCurrentGame();
    }

    EditorGUILayout.EndHorizontal();

    EditorGUI.EndDisabledGroup();
  }
}