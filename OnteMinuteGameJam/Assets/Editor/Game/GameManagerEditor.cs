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

    if (GUILayout.Button("StartNewGame")) {
      _gameManager.StartNewGame();
    }

    EditorGUI.EndDisabledGroup();
  }
}