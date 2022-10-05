using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
  [field: SerializeField, Header("Managers")]
  public MoleManager MoleManager { get; private set; }

  [field: SerializeField]
  public GameManager GameManager { get; private set; }

  public void Start() {
    Scene scene = SceneManager.GetSceneByName("1.5-UI");

    if (!scene.IsValid()) {
      scene = SceneManager.LoadScene("1.5-UI", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    GameManager =
        scene.GetRootGameObjects()
            .Where(go => go && go.CompareTag("GameManager"))
            .Select(go => go.GetComponent<GameManager>())
            .FirstOrDefault();

    if (GameManager) {
      // If needed.
    } else {
      Debug.LogError($"Could not find GameManager after scene load.");
    }
  }
}