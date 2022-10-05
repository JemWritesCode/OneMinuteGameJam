using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
      if (SceneManager.GetSceneByName("1.5-UI").IsValid()) {
        // Scene already loaded.
      } else {
        SceneManager.LoadScene("1.5-UI", LoadSceneMode.Additive);
      }
    }
}
