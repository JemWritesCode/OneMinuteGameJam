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

  private MouseClickListener _mouseClickListener;
  private int _currentScore = 0;

  public void StartNewGame() {
    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }

    _mouseClickListener = gameObject.AddComponent<MouseClickListener>();

    _mouseClickListener.OnLeftMouseButtonDown += (_, position) => {
      int points = Random.Range(1, 5) * 100;

      ScoreController.LerpScoreValue(
          _currentScore, _currentScore + points, 0.5f, ScoreIncreaseColor, ScoreIncreaseFontSizeOffset);

      _currentScore += points;

      PopupController.PopupHit(position, $"+{points:N0}");
    };

    _mouseClickListener.OnRightMouseButtonDown += (_, position) => {
      int points = Random.Range(1, 5) * 100;

      ScoreController.LerpScoreValue(
          _currentScore, _currentScore - points, 0.5f, ScoreDecreaseColor, ScoreDecreaseFontSizeOffset);

      _currentScore -= points;

      PopupController.PopupMiss(position, $"-{points:N0}");
    };

    _currentScore = 0;

    ScoreController.SetScoreValue(_currentScore);
    TimerController.StartTimer(60f, 0f);
  }
}