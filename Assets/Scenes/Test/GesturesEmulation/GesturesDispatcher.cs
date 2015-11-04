﻿using UnityEngine;
using System;

public class GesturesDispatcher : MonoBehaviour {

    public static event Action<Gesture> OnGestureStart;
    public static event Action<Gesture> OnGestureEnd;
    public static event Action<Gesture> OnGestureProgress;

    public bool DebugMode;

    void Awake() {
        if (DebugMode) {
            GesturesEmulator.OnClick += pos => OnGestureEnd(new Tap(pos, 0f));
            GesturesEmulator.OnDrag += (start, end, time) => OnGestureEnd(swipe(start, end, time));
            GesturesEmulator.OnZoomIn += () => OnGestureEnd(spread());
            GesturesEmulator.OnZoomOut += () => OnGestureEnd(pinch());
        } else {
            var recogniser = GestureRecogniser.Recogniser;
            recogniser.subscribeEnd(gesture => OnGestureEnd(gesture));
            recogniser.subscribeProgress(gesture => OnGestureProgress(gesture));
        }
    }

    Swipe swipe(Vector2 start, Vector2 end, float time) {
        var swipe = new Swipe(start);
        swipe.End = end;
        // TODO: swipe.time = time
        return swipe;
    }

    Sprinch pinch() {
        var pinch = new Sprinch(Vector2.zero, new Vector2(100f, 100f));
        pinch.EndPoints = new Vector2[] { Vector2.zero, Vector2.zero };
        return pinch;
    }

    Sprinch spread() {
        var spread = new Sprinch(Vector2.zero, Vector2.zero);
        spread.EndPoints = new Vector2[] { Vector2.zero, new Vector2(100f, 100f) };
        return spread;
    }
}
