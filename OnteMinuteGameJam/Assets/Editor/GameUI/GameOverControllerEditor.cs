﻿using UnityEditor;

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

  private int _highestComboValue = 0;
  private int _finalScoreValue = 0;
  private int _pumpkinsTotal = 0;

  private void ShowHideGameOverGUI() {
    _highestComboValue = EditorGUILayout.IntField("HighestComboValue", _highestComboValue);
    _finalScoreValue = EditorGUILayout.IntField("FinalScoreValue", _finalScoreValue);
    _pumpkinsTotal = EditorGUILayout.IntField("PumpkinsTotal", _pumpkinsTotal);

    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("ShowGameOver")) {
      _gameOverController.ShowGameOver(_highestComboValue, _finalScoreValue, _pumpkinsTotal);
    }

    if (GUILayout.Button("HideGameOver")) {
      _gameOverController.HideGameOver();
    }

    EditorGUILayout.EndHorizontal();
  }
}
