using UnityEngine;
using UnityEngine.UI;

public class GesturesLogger : MonoBehaviour {

    Text log;
    int line;

	void Start () {
        log = GetComponent<Text>();
        log.text = "";
        GesturesEmulator.OnClick += LogClick;
        GesturesEmulator.OnDrag += LogDrag;
        GesturesEmulator.OnZoomIn += LogZoomIn;
        GesturesEmulator.OnZoomOut += LogZoomOut;
    }

    public void LogClick(Vector2 position) {
        log.text = string.Format("[{0}] CLICK @ {1}\n", ++line, position) + log.text;
    }

    public void LogDrag(Vector2 start, Vector2 end, float time) {
        log.text = string.Format("[{0}] DRAG {1} -> {2} in {3}s\n", ++line, start, end, time) + log.text;
    }

    public void LogZoomIn() {
        log.text = string.Format("[{0}] ZOOM IN\n", ++line) + log.text;
    }

    public void LogZoomOut() {
        log.text = string.Format("[{0}] ZOOM OUT\n", ++line) + log.text;
    }
}

