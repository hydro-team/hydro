using UnityEngine;
using System.Collections;

public class MouseGestures : MonoBehaviour {

    static MouseGestures instance;

    public int Button;
    public float MinDragSpan;

    Vector2 pressPosition;
    Vector2 releasePosition;
    bool leftButtonPressed = false;

    public static MouseGestures Instance() {
        return instance;
    }

    void Awake() {
        if (instance == null) { instance = this; }
        else { GameObject.Destroy(this); }
    }

    void Update() {
        if (leftButtonPressed) {
            leftButtonPressed = Input.GetMouseButton(Button);
            if (!leftButtonPressed) {
                releasePosition = Input.mousePosition;
                var type = (releasePosition - pressPosition).magnitude < MinDragSpan ? Type.CLICK : Type.DRAG;
                Debug.Log("event: " + type + " (" + pressPosition + " -> " + releasePosition + ")");
                if (MouseEvent != null) { MouseEvent(type, pressPosition, releasePosition); }
            }
        } else {
            leftButtonPressed = Input.GetMouseButton(Button);
            if (leftButtonPressed) {
                pressPosition = Input.mousePosition;
            }
        }
    }

    public enum Type {
        CLICK, DRAG
    }

    public delegate void MouseActionHandler(Type type, Vector2 from, Vector2 to);

    public event MouseActionHandler MouseEvent;
}
