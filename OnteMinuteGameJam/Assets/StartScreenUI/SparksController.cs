using DG.Tweening;

using UnityEngine;

public class SparksController : MonoBehaviour {
  [field: SerializeField]
  public GameObject LeftClickSpark { get; private set; }

  private Camera _camera;

  public void Start() {
    _camera = Camera.main;
  }

  public void Update() {
    if (Input.GetMouseButtonDown(0)) {
      Vector3 targetPosition =
          _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f));
      GameObject spark = Instantiate(LeftClickSpark, targetPosition, Quaternion.identity);
      DOTween.Sequence().InsertCallback(1.5f, () => Destroy(spark));
    }
  }
}
