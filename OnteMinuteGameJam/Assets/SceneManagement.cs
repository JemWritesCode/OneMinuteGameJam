using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
  private GameManager _gameManager;

  void Start() {
    Scene scene = SceneManager.GetSceneByName("1.5-UI");

    if (!scene.IsValid()) {
      scene = SceneManager.LoadScene("1.5-UI", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    _gameManager =
        scene.GetRootGameObjects()
            .Where(go => go && go.CompareTag("GameManager"))
            .Select(go => go.GetComponent<GameManager>())
            .FirstOrDefault();

    if (!_gameManager) {
      Debug.LogError($"Could not find GameManager after scene load.");
    }
  }
}