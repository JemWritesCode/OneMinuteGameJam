using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
  public void Start() {
    if (!SceneManager.GetSceneByName("1.5-UI").IsValid()) {
      SceneManager.LoadScene("1.5-UI", new LoadSceneParameters(LoadSceneMode.Additive));
    }
  }
}