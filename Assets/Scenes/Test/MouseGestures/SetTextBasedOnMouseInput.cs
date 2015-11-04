using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTextBasedOnMouseInput : MonoBehaviour {

    Text text;
    int line;

	void Start () {
        text = GetComponent<Text>();
        text.text = "";
        line = 0;
        MouseGestures.Instance().MouseEvent += LogMouseEvent;
	}

    public void LogMouseEvent(MouseGestures.Type type, Vector2 start, Vector2 end) {
        text.text = string.Format("[{5}] type: {0}, ({1} -> {2}), direction: {3}, length: {4}\n",
            type,
            start,
            end,
            end - start,
            (end - start).magnitude,
            ++line) + text.text;
        Debug.DrawLine(start, end, Color.green, 1f);
    }
}
