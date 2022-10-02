using DG.Tweening;

using UnityEditor;

using UnityEngine;

public class ScoreController : MonoBehaviour {
  [field: SerializeField, Header("TMP")]
  public TMPro.TMP_Text ScoreLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text ScoreValue { get; private set; }

  public void SetScoreValue(int value) {
    DOTween.Kill(ScoreValue.GetInstanceID());
    ScoreValue.SetText($"{value:G0}");
  }

  public void LerpScoreValue(int startValue, int endValue, float duration) {
    DOTween.Kill(ScoreValue.GetInstanceID());

    DOVirtual
        .Int(startValue, endValue, duration, v => ScoreValue.SetText($"{v:G0}"))
        .SetLink(ScoreValue.gameObject)
        .SetId(ScoreValue.GetInstanceID());
  }
}

[CustomEditor(typeof(ScoreController))]
public class ScoreControllerEditor : Editor {
  private ScoreController _scoreController;

  public void OnEnable() {
    _scoreController = Selection.activeGameObject.GetComponent<ScoreController>();
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUI.BeginDisabledGroup(!Application.isPlaying);

    EditorGUILayout.Space(25f);
    SetScoreValueGUI();

    EditorGUILayout.Space(25f);
    LerpScoreValueGUI();

    EditorGUI.EndDisabledGroup();
  }

  private bool _setScoreHeaderGroup = true;
  private int _setScoreValue = 0;

  private void SetScoreValueGUI() {
    _setScoreHeaderGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_setScoreHeaderGroup, "SetScoreValue");

    if (_setScoreHeaderGroup) {
      _setScoreValue = EditorGUILayout.IntField("Value", _setScoreValue);

      if (GUILayout.Button("SetScoreValue", GUILayout.MinWidth(125f))) {
        _scoreController.SetScoreValue(_setScoreValue);
      }
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private bool _lerpScoreHeaderGroup = true;
  private int _lerpScoreStartValue = 0;
  private int _lerpScoreEndValue = 100;
  private float _lerpScoreDuration = 0.5f;

  private void LerpScoreValueGUI() {
    _lerpScoreHeaderGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_lerpScoreHeaderGroup, "LerpScoreValue");

    if (_lerpScoreHeaderGroup) {
      _lerpScoreStartValue = EditorGUILayout.IntField("StartValue", _lerpScoreStartValue);
      _lerpScoreEndValue = EditorGUILayout.IntField("EndValue", _lerpScoreEndValue);
      _lerpScoreDuration = EditorGUILayout.Slider("Duration", _lerpScoreDuration, 0f, 5f);

      if (GUILayout.Button("LerpScoreValue", GUILayout.MinWidth(125f))) {
        _scoreController.LerpScoreValue(_lerpScoreStartValue, _lerpScoreEndValue, _lerpScoreDuration);
      }
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }
}