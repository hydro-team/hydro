using UnityEngine;
using UnityEngine.UI;
using Gestures;

public class GesturesLogger : MonoBehaviour {

    void Start() {
        var log = GetComponent<Text>();
        log.text = "";
        GesturesDispatcher.OnTapStart += tap => log.text = "TAP START: position=" + tap.Position;
        GesturesDispatcher.OnTapEnd += tap => log.text = "TAP END: position=" + tap.Position;
        GesturesDispatcher.OnSwipeStart += swipe => log.text = "SWIPE START: " + swipe.Start + " -> " + swipe.End;
        GesturesDispatcher.OnSwipeProgress += swipe => log.text = "SWIPE PROGRESS: " + swipe.Start + " -> " + swipe.End;
        GesturesDispatcher.OnSwipeEnd += swipe => log.text = "SWIPE END: " + swipe.Start + " -> " + swipe.End;
        GesturesDispatcher.OnSprinchStart += sprinch => log.text = "SPRINCH START: percentage=" + sprinch.Percentage + ", canceled=" + sprinch.Canceled;
        GesturesDispatcher.OnSprinchProgress += sprinch => log.text = "SPRINCH PROGRESS: percentage=" + sprinch.Percentage + ", canceled=" + sprinch.Canceled;
        GesturesDispatcher.OnPinchStart += pinch => log.text = "PINCH START: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        GesturesDispatcher.OnPinchProgress += pinch => log.text = "PINCH PROGRESS: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        GesturesDispatcher.OnPinchEnd += pinch => log.text = "PINCH END: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        GesturesDispatcher.OnSpreadStart += spread => log.text = "SPREAD START: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
        GesturesDispatcher.OnSpreadProgress += spread => log.text = "SPREAD PROGRESS: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
        GesturesDispatcher.OnSpreadEnd += spread => log.text = "SPREAD END: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
    }
}

