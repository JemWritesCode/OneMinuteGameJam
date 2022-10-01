using DG.Tweening;

using UnityEditor;

using UnityEngine;

public class TimerController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text TimerValue { private set; get; }

  public void StartTimer(float startValue, float endValue) {
    DOTween.Kill(TimerValue.GetInstanceID());

    DOVirtual
        .Float(startValue, endValue, Mathf.Abs(endValue - startValue), v => TimerValue.SetText($"{v:N2}"))
        .SetLink(TimerValue.gameObject)
        .SetId(TimerValue.GetInstanceID());
  }

  public void StopTimer() {
    DOTween.Kill(TimerValue.GetInstanceID());
  }
}

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
    StartTimerGUI();

    EditorGUILayout.Space(25f);
    StopTimerGUI();

    EditorGUI.EndDisabledGroup();
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
        _timerController.StartTimer(_startTimerStartValue, _startTimerEndValue);
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