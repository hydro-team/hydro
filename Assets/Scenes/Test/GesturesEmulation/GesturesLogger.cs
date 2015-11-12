using UnityEngine;
using UnityEngine.UI;
using Gestures;

public class GesturesLogger : MonoBehaviour {

    public GameObject gestures;

    void Start() {
        var log = GetComponent<Text>();
        log.text = "";
        var dispatcher = gestures.GetComponent<GesturesDispatcher>();
        dispatcher.OnTapStart += tap => log.text = "TAP START: position=" + tap.Position;
        dispatcher.OnTapEnd += tap => log.text = "TAP END: position=" + tap.Position;
        dispatcher.OnSwipeStart += swipe => log.text = "SWIPE START: " + swipe.Start + " -> " + swipe.End;
        dispatcher.OnSwipeProgress += swipe => log.text = "SWIPE PROGRESS: " + swipe.Start + " -> " + swipe.End;
        dispatcher.OnSwipeEnd += swipe => log.text = "SWIPE END: " + swipe.Start + " -> " + swipe.End;
        dispatcher.OnSprinchStart += sprinch => log.text = "SPRINCH START: percentage=" + sprinch.Percentage + ", canceled=" + sprinch.Canceled;
        dispatcher.OnSprinchProgress += sprinch => log.text = "SPRINCH PROGRESS: percentage=" + sprinch.Percentage + ", canceled=" + sprinch.Canceled;
        dispatcher.OnPinchStart += pinch => log.text = "PINCH START: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        dispatcher.OnPinchProgress += pinch => log.text = "PINCH PROGRESS: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        dispatcher.OnPinchEnd += pinch => log.text = "PINCH END: percentage=" + pinch.Percentage + ", canceled=" + pinch.Canceled;
        dispatcher.OnSpreadStart += spread => log.text = "SPREAD START: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
        dispatcher.OnSpreadProgress += spread => log.text = "SPREAD PROGRESS: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
        dispatcher.OnSpreadEnd += spread => log.text = "SPREAD END: percentage=" + spread.Percentage + ", canceled=" + spread.Canceled;
    }
}

