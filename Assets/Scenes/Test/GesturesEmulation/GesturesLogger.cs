using UnityEngine;
using UnityEngine.UI;

public class GesturesLogger : MonoBehaviour {

    Text log;
    int line;

    void Start() {
        log = GetComponent<Text>();
        log.text = "";
        GesturesDispatcher.OnGestureStart += LogGestureStart;
        GesturesDispatcher.OnGestureEnd += LogGestureEnd;
        GesturesDispatcher.OnGestureProgress += LogGestureProgress;
    }

    public void LogGestureStart(Gesture gesture) {
        log.text = "STARTED: " + gesture.Type;
        LogGesture(gesture);
    }

    public void LogGestureEnd(Gesture gesture) {
        log.text = "ENDED: " + gesture.Type;
        LogGesture(gesture);
    }

    public void LogGestureProgress(Gesture gesture) {
        log.text = "IN PROGRESS: " + gesture.Type;
        LogGesture(gesture);
    }

    void LogGesture(Gesture gesture) {
        Log(gesture as Tap);
        Log(gesture as Swipe);
        Log(gesture as Sprinch);
    }

    void Log(Tap tap) {
        if (tap != null) { log.text += ": position=" + tap.Position; }
    }

    void Log(Swipe swipe) {
        if (swipe != null) { log.text += ": " + swipe.Start + " -> " + swipe.End; }
    }

    void Log(Sprinch sprinch) {
        if (sprinch != null) { log.text += ": percentage=" + sprinch.Percentage + ", canceled=" + sprinch.Canceled; }
    }
}

