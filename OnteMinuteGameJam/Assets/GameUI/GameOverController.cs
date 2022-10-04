using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameOverController : MonoBehaviour {
  [field: SerializeField]
  public PostProcessVolume CameraEffect { get; private set; }

  public void ShowGameOver() {
    CameraEffect.enabled = true;
  }

  public void HideGameOver() {
    CameraEffect.enabled = false;
  }
}
