using System;

using UnityEngine;

public class MouseClickListener : MonoBehaviour {
  public event EventHandler<Vector2> OnLeftMouseButtonDown;
  public event EventHandler<Vector2> OnRightMouseButtonDown;
  public event EventHandler<Vector2> OnCenterMouseButtonDown;

  public void Update() {
    if (Input.GetMouseButtonDown(0)) {
      OnLeftMouseButtonDown?.Invoke(this, Input.mousePosition);
    }

    if (Input.GetMouseButtonDown(1)) {
      OnRightMouseButtonDown?.Invoke(this, Input.mousePosition);
    }

    if (Input.GetMouseButtonDown(2)) {
      OnCenterMouseButtonDown?.Invoke(this, Input.mousePosition);
    }
  }
}