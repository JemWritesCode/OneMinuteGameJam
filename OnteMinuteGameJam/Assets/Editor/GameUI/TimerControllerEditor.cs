using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(TimerController))]
public class TimerControllerEditor : Editor {
  private TimerController _timerController;

  public void OnEnable() {
    _timerController = Selection.activeGameObject.GetComponent<TimerController>();
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);

    EditorGUILayout.Space(25f);
    AnimateGUI();

    EditorGUILayout.Space(25f);
    StartTimerGUI();

    EditorGUILayout.Space(25f);
    StopTimerGUI();

    EditorGUI.EndDisabledGroup();
  }

  private void AnimateGUI() {
    if (GUILayout.Button("AnimateIn")) {
      _timerController.AnimateIn();
    }
  }

  private bool _startTimerHeaderGroup = true;
  private float _startTimerStartValue = 60f;
  private float _startTimerEndValue = 0f;

  private void StartTimerGUI() {
    _startTimerHeaderGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_startTimerHeaderGroup, "StartTimer");

    if (_startTimerHeaderGroup) {
      _startTimerStartValue = EditorGUILayout.FloatField("StartValue", _startTimerStartValue);
      _startTimerEndValue = EditorGUILayout.FloatField("EndValue", _startTimerEndValue);

      if (GUILayout.Button("StartTimer", GUILayout.MinWidth(125f))) {
        _timerController.StartTimer(_startTimerStartValue, _startTimerEndValue, () => { });
      }
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private bool _stopTimerHeaderGroup = true;

  private void StopTimerGUI() {
    _stopTimerHeaderGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_stopTimerHeaderGroup, "StopTimer");

    if (_stopTimerHeaderGroup) {
      if (GUILayout.Button("StopTimer")) {
        _timerController.StopTimer();
      }
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }
}