using DG.Tweening;

using UnityEngine;

public class GameManager : MonoBehaviour {
  [field: SerializeField]
  public Canvas TargetCanvas { get; private set; }

  [field: SerializeField, Header("Score")]
  public ScoreController ScoreController { get; private set; }

  [field: SerializeField]
  public Color ScoreIncreaseColor { get; private set; }

  [field: SerializeField]
  public float ScoreIncreaseFontSizeOffset { get; private set; }

  [field: SerializeField]
  public Color ScoreDecreaseColor { get; private set; }

  [field: SerializeField]
  public float ScoreDecreaseFontSizeOffset { get; private set; }

  [field: SerializeField, Header("Timer")]
  public TimerController TimerController { get; private set; }

  [field: SerializeField, Header("Popup")]
  public PopupController PopupController { get; private set; }

  [field: SerializeField, Header("GameOver")]
  public GameOverController GameOverController { get; private set; }

  private Camera _targetCamera;
  private MouseClickListener _mouseClickListener;

  private int _currentScore = 0;
  private int _currentCombo = 0;

  public void StartNewGame() {
    if (!_targetCamera) {
      _targetCamera = Camera.main;
    }

    GameOverController.HideGameOver();

    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }

    _mouseClickListener = gameObject.AddComponent<MouseClickListener>();
    _mouseClickListener.OnLeftMouseButtonDown += (_, position) => ProcessLeftClick(position);
    _mouseClickListener.OnCenterMouseButtonDown += (_, position) => ProcessHit(position, Random.Range(1, 5) * 100);
    _mouseClickListener.OnRightMouseButtonDown += (_, position) => ProcessMiss(position, Random.Range(1, 5) * 100);


    _currentScore = 0;
    _currentCombo = 0;

    ScoreController.SetScoreValue(_currentScore);
    TimerController.StartTimer(60f, 0f);
  }

  public void StopCurrentGame() {
    TimerController.StopTimer();

    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }

    GameOverController.ShowGameOver();
  }

  public void ProcessLeftClick(Vector2 mousePosition) {
    Ray ray = _targetCamera.ScreenPointToRay(mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 20f, Color.yellow);

    if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f)) {
      if (hitInfo.collider.CompareTag("Mole")) {
        Debug.Log($"Hit mole! {mousePosition} -> {hitInfo.collider.name}: {hitInfo.point}");
        ProcessHit(mousePosition, 100 + (_currentCombo * 100));
        hitInfo.collider.GetComponent<MoleDeath>().MoleDied();
      }
    }
  }

  public void ProcessHit(Vector2 popupPosition, int pointsGained) {
    ScoreController.LerpScoreValue(
        _currentScore, _currentScore + pointsGained, 0.5f, ScoreIncreaseColor, ScoreIncreaseFontSizeOffset);

    _currentScore += pointsGained;
    _currentCombo++;

    PopupController.PopupHit(popupPosition, $"+{pointsGained:N0}");

    if (_currentCombo >= 3) {
      PopupController.PopupCombo(Vector2.zero, $"{_currentCombo}<sup>Combo</sup>");
    }
  }

  public void ProcessMiss(Vector2 popupPosition, int pointsLost) {
    ScoreController.LerpScoreValue(
        _currentScore, _currentScore - pointsLost, 0.5f, ScoreDecreaseColor, ScoreDecreaseFontSizeOffset);

    _currentScore -= pointsLost;

    PopupController.PopupMiss(popupPosition, $"-{pointsLost:N0}");

    if (_currentCombo >= 3) {
      PopupController.PopupComboBroken(Vector2.zero, $"{_currentCombo++}<sup>Broken!</sup>");
    }

    _currentCombo = 0;
  }
}