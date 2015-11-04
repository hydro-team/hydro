using UnityEngine;
using UnityEngine.UI;

public class GesturesLogger : MonoBehaviour {

    Text log;
    int line;

	void Start () {
        log = GetComponent<Text>();
        log.text = "";
        GesturesDispatcher.OnGestureStart += LogGestureStart;
        GesturesDispatcher.OnGestureEnd += LogGestureEnd;
        GesturesDispatcher.OnGestureProgress += LogGestureProgress;
    }

    public void LogGestureStart(Gesture gesture) {
        log.text = "STARTED: " + gesture.Type;
    }

    public void LogGestureEnd(Gesture gesture) {
        log.text = "ENDED: " + gesture.Type;
    }

    public void LogGestureProgress(Gesture gesture) {
        log.text = "IN PROGRESS: " + gesture.Type;
    }
}

