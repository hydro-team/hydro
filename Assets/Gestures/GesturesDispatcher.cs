using UnityEngine;
using System;

namespace Gestures {

    /// <summary>
    /// Defines all gesture event types, and dispatches the events triggered by
    /// the GestureRecogniser (or by the GesturesEmulator if in debug mode).
    /// </summary>
    public class GesturesDispatcher : MonoBehaviour {

        public GameObject gesturesRecognizer;
        public GameObject gesturesEmulator;

        public event Action<Gesture> OnGestureStart, OnGestureProgress, OnGestureEnd;
        public event Action<Tap> OnTapStart, OnTapEnd;
        public event Action<Swipe> OnSwipeStart, OnSwipeProgress, OnSwipeEnd;
        public event Action<Sprinch> OnSprinchStart, OnSprinchProgress;
        public event Action<Sprinch> OnPinchStart, OnPinchProgress, OnPinchEnd;
        public event Action<Sprinch> OnSpreadStart, OnSpreadProgress, OnSpreadEnd;

        void Awake() {
            var debug = !(Application.platform == RuntimePlatform.Android);
            if (debug) {
                var emulator = gesturesEmulator.GetComponent<GesturesEmulator>();
                emulator.OnClick += pos => NotifyGestureEnd(new Tap(pos, 0f));
                emulator.OnDrag += (start, end, time) => NotifyGestureEnd(Swipe(start, end, time));
                emulator.OnZoomIn += () => NotifyGestureEnd(Spread());
                emulator.OnZoomOut += () => NotifyGestureEnd(Pinch());
            } else {
                var recogniser = gesturesRecognizer.GetComponent<GesturesRecogniser>();
                recogniser.GestureStart += NotifyGestureStart;
                recogniser.GestureProgress += NotifyGestureProgress;
                recogniser.GestureEnd += NotifyGestureEnd;
            }
        }

        void NotifyGestureStart(Gesture gesture) {
            Trigger(OnGestureStart, gesture);
            switch (gesture.Type) {
                case GestureType.TAP: Trigger(OnTapStart, gesture as Tap); break;
                case GestureType.SWIPE: Trigger(OnSwipeStart, gesture as Swipe); break;
                case GestureType.SPRINCH: Trigger(OnSprinchStart, gesture as Sprinch); break;
                case GestureType.PINCH: Trigger(OnPinchStart, gesture as Sprinch); break;
                case GestureType.SPREAD: Trigger(OnSpreadStart, gesture as Sprinch); break;
            }
        }

        void NotifyGestureProgress(Gesture gesture) {
            Trigger(OnGestureProgress, gesture);
            switch (gesture.Type) {
                case GestureType.SWIPE: Trigger(OnSwipeProgress, gesture as Swipe); break;
                case GestureType.SPRINCH: Trigger(OnSprinchProgress, gesture as Sprinch); break;
                case GestureType.PINCH: Trigger(OnPinchProgress, gesture as Sprinch); break;
                case GestureType.SPREAD: Trigger(OnSpreadProgress, gesture as Sprinch); break;
            }
        }

        void NotifyGestureEnd(Gesture gesture) {
            Trigger(OnGestureEnd, gesture);
            switch (gesture.Type) {
                case GestureType.TAP: Trigger(OnTapEnd, gesture as Tap); break;
                case GestureType.SWIPE: Trigger(OnSwipeEnd, gesture as Swipe); break;
                case GestureType.PINCH: Trigger(OnPinchEnd, gesture as Sprinch); break;
                case GestureType.SPREAD: Trigger(OnSpreadEnd, gesture as Sprinch); break;
            }
        }

        static void Trigger<T>(Action<T> handler, T value) {
            if (handler != null) { handler.Invoke(value); }
        }

        Swipe Swipe(Vector2 start, Vector2 end, float time) {
            var swipe = new Swipe(start);
            swipe.End = end;
            // TODO: swipe.time = time
            return swipe;
        }

        Sprinch Pinch() {
            var pinch = new Sprinch(Vector2.zero, new Vector2(100f, 100f));
            pinch.EndPoints = new Vector2[] { Vector2.zero, Vector2.zero };
            return pinch;
        }

        Sprinch Spread() {
            var spread = new Sprinch(Vector2.zero, Vector2.zero);
            spread.EndPoints = new Vector2[] { Vector2.zero, new Vector2(100f, 100f) };
            return spread;
        }
    }
}
