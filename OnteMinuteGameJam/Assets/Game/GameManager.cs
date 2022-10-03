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

  private Camera _targetCamera;
  private MouseClickListener _mouseClickListener;
  private int _currentScore = 0;

  public void StartNewGame() {
    _targetCamera = Camera.main;

    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }

    _mouseClickListener = gameObject.AddComponent<MouseClickListener>();
    _mouseClickListener.OnLeftMouseButtonDown += (_, position) => ProcessHit(position, Random.Range(1, 5) * 100);
    _mouseClickListener.OnLeftMouseButtonDown += (_, position) => ProcessLeftClick(position);
    _mouseClickListener.OnRightMouseButtonDown += (_, position) => ProcessMiss(position, Random.Range(1, 5) * 100);

    _currentScore = 0;

    ScoreController.SetScoreValue(_currentScore);
    TimerController.StartTimer(60f, 0f);
  }

  public void StopCurrentGame() {
    TimerController.StopTimer();

    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }
  }

  public void ProcessLeftClick(Vector2 mousePosition) {
    Ray ray = _targetCamera.ScreenPointToRay(mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 20f, Color.yellow);

    if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f)) {
      //Debug.Log($"{mousePosition} -> ({hitInfo.collider.name}: {hitInfo.point})");
      //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      //sphere.transform.localScale = Vector3.zero;
      //sphere.transform.SetPositionAndRotation(hitInfo.point, Quaternion.identity);
      //Destroy(sphere.GetComponentInChildren<Collider>());
      //sphere.transform.DOScale(Vector3.one, 3f).SetLink(sphere).OnComplete(() => Destroy(sphere));
    }
  }

  public void ProcessHit(Vector2 popupPosition, int pointsGained) {
    ScoreController.LerpScoreValue(
        _currentScore, _currentScore + pointsGained, 0.5f, ScoreIncreaseColor, ScoreIncreaseFontSizeOffset);

    _currentScore += pointsGained;

    PopupController.PopupHit(popupPosition, $"+{pointsGained:N0}");
  }

  public void ProcessMiss(Vector2 popupPosition, int pointsLost) {
    ScoreController.LerpScoreValue(
        _currentScore, _currentScore - pointsLost, 0.5f, ScoreDecreaseColor, ScoreDecreaseFontSizeOffset);

    _currentScore -= pointsLost;

    PopupController.PopupMiss(popupPosition, $"-{pointsLost:N0}");
  }
}