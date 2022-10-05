using System.Linq;

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

  [field: SerializeField, Min(0f), Header("GameSettings")]
  public float GameTotalTime { get; private set; }

  [field: SerializeField, Header("Mole")]
  public MoleManager MoleManager { get; private set; }

  private Camera _targetCamera;
  private MouseClickListener _mouseClickListener;

  private int _currentHits = 0;
  private int _currentScore = 0;
  private int _currentCombo = 0;
  private int _highestCombo = 0;

  public void Start() {
    if (!MoleManager) {
      MoleManager = FindObjectsOfType<MoleManager>().FirstOrDefault();
    }

    StartNewGame();
  }

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

    if (Application.isEditor) {
      _mouseClickListener.OnCenterMouseButtonDown += (_, position) => ProcessHit(position, Random.Range(1, 5) * 100);
      _mouseClickListener.OnRightMouseButtonDown += (_, position) => ProcessMiss(position, Random.Range(1, 5) * 100);
    }

    MoleManager.OnMoleUpDownEnd -= OnMoleMiss;
    MoleManager.OnMoleUpDownEnd += OnMoleMiss;

    _currentHits = 0;
    _currentScore = 0;
    _currentCombo = 0;
    _highestCombo = 0;

    ScoreController.SetScoreValue(_currentScore);
    TimerController.StartTimer(GameTotalTime, 0f, () => StopCurrentGame());
  }

  public void StopCurrentGame() {
    TimerController.StopTimer();

    if (_mouseClickListener) {
      Destroy(_mouseClickListener);
    }

    MoleManager.OnMoleUpDownEnd -= OnMoleMiss;

    int pumpkinsTotal = MoleManager.EnemiesSpawnedCount;
    int pumpkinsOnBoard = GameObject.FindGameObjectsWithTag("Mole").Where(go => go.activeInHierarchy).Count();

    Debug.Log($"EnemiesSpawnedCount {pumpkinsTotal}, PumpkinsOnBoard: {pumpkinsOnBoard}");

    GameOverController.ShowGameOver(
        _currentScore,
        _highestCombo,
        pumpkinsTotal - pumpkinsOnBoard,
        _currentHits);
  }

  public void ProcessLeftClick(Vector2 mousePosition) {
    Ray ray = _targetCamera.ScreenPointToRay(mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 20f, Color.yellow);

    if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f)) {
      if (hitInfo.collider.CompareTag("Mole")) {
        Debug.Log($"Hit mole! {mousePosition} -> {hitInfo.collider.name}: {hitInfo.point}");
        ProcessHit(mousePosition, 100 + (_currentCombo * 100));
        hitInfo.collider.GetComponent<MoleDeath>().KillMole();
      }
    }
  }

  public void ProcessHit(Vector2 popupPosition, int pointsGained) {
    ScoreController.LerpScoreValue(
        _currentScore, _currentScore + pointsGained, 0.5f, ScoreIncreaseColor, ScoreIncreaseFontSizeOffset);

    _currentHits++;
    _currentScore += pointsGained;
    _currentCombo++;

    PopupController.PopupHit(popupPosition, $"+{pointsGained:N0}");

    if (_currentCombo >= 3) {
      PopupController.PopupCombo(Vector2.zero, $"{_currentCombo}<sup>Combo</sup>");
    }

    if (_currentCombo > _highestCombo) {
      _highestCombo = _currentCombo;
    }
  }
  
  private void OnMoleMiss(object sender, Vector3 molePosition) {
    Vector3 popupPosition = _targetCamera.WorldToScreenPoint(molePosition);
    Debug.Log($"Mole miss: {molePosition} -> {popupPosition}");

    ProcessMiss(popupPosition, 100);
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