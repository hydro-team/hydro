using UnityEngine;
using System;

public class GesturesEmulator : MonoBehaviour {

    static GesturesEmulator instance;

    public float MinDragSpan;

    const int LEFT_BUTTON = 0;

    bool dragging;
    Vector2 dragStartPosition;
    float dragStartTime;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            GameObject.Destroy(this);
        }
    }

    public static event Action<Vector2> OnClick;
    public static event Action<Vector2, Vector2, float> OnDrag;
    public static event Action OnZoomIn;
    public static event Action OnZoomOut;

    void Update() {
        if (Input.GetMouseButtonDown(LEFT_BUTTON)) {
            dragging = true;
            dragStartPosition = Input.mousePosition;
            dragStartTime = Time.time;
        }
        if (dragging && Input.GetMouseButtonUp(LEFT_BUTTON)) {
            dragging = false;
            Vector2 dragEndPosition = Input.mousePosition;
            var span = (dragEndPosition - dragStartPosition).magnitude;
            if (span < MinDragSpan) {
                if (OnClick != null) { OnClick(dragStartPosition); }
            } else {
                if (OnDrag != null) { OnDrag(dragStartPosition, dragEndPosition, Time.time - dragStartTime); }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            if (Input.GetKey(KeyCode.W)) { if (OnZoomIn != null) { OnZoomIn(); } }
            if (Input.GetKey(KeyCode.S)) { if (OnZoomOut != null) { OnZoomOut(); } }
        }
    }
}
