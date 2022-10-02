using UnityEngine;

public class GameManager : MonoBehaviour {
  [field: SerializeField]
  public Canvas TargetCanvas { get; private set; }

  [field: SerializeField]
  public ScoreController ScoreController { get; private set; }
  
  [field: SerializeField]
  public TimerController TimerController { get; private set; }

  [field: SerializeField]
  public PopupController PopupController { get; private set; }

  private GameObject _gameState;
  private MouseClickListener _mouseClickListener;

  public void StartNewGame() {
    if (_gameState) {
      Destroy(_gameState);
    }

    _gameState = new("GameState");
    _mouseClickListener = _gameState.AddComponent<MouseClickListener>();

    _mouseClickListener.OnLeftMouseButtonDown += (_, position) => {
      PopupController.PopupHit(position, $"+{(Random.Range(1, 5) * 100):N0}");
    };

    _mouseClickListener.OnRightMouseButtonDown += (_, position) => {
      PopupController.PopupMiss(position, $"-{(Random.Range(1, 5) * 100):N0}");
    };

    ScoreController.SetScoreValue(0);
    TimerController.StartTimer(60f, 0f);
  }
}