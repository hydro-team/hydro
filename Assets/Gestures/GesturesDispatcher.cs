using UnityEngine;
using System;

namespace Gestures {

    /// <summary>
    /// Defines all gesture event types, and dispatches the events triggered by
    /// the GestureRecogniser (or by the GesturesEmulator if in debug mode).
    /// </summary>
    public class GesturesDispatcher : MonoBehaviour {

        public static event Action<Gesture> OnGestureStart;
        public static event Action<Gesture> OnGestureEnd;
        public static event Action<Gesture> OnGestureProgress;

        public static event Action<Tap> OnTapStart;
        public static event Action<Tap> OnTapEnd;

        public static event Action<Swipe> OnSwipeStart;
        public static event Action<Swipe> OnSwipeEnd;
        public static event Action<Swipe> OnSwipeProgress;

        public static event Action<Sprinch> OnSprinchStart;
        public static event Action<Sprinch> OnSprinchProgress;

        public static event Action<Sprinch> OnPinchStart;
        public static event Action<Sprinch> OnPinchProgress;
        public static event Action<Sprinch> OnPinchEnd;

        public static event Action<Sprinch> OnSpreadStart;
        public static event Action<Sprinch> OnSpreadProgress;
        public static event Action<Sprinch> OnSpreadEnd;

        void Awake() {
            var debug = !(Application.platform == RuntimePlatform.Android);
            if (debug) {
                GesturesEmulator.OnClick += pos => NotifyGestureEnd(new Tap(pos, 0f));
                GesturesEmulator.OnDrag += (start, end, time) => NotifyGestureEnd(swipe(start, end, time));
                GesturesEmulator.OnZoomIn += () => NotifyGestureEnd(spread());
                GesturesEmulator.OnZoomOut += () => NotifyGestureEnd(pinch());
            } else {
                GestureRecogniser.GestureStart += NotifyGestureStart;
                GestureRecogniser.GestureProgress += NotifyGestureProgress;
                GestureRecogniser.GestureEnd += NotifyGestureEnd;
            }
        }

        void NotifyGestureStart(Gesture gesture) {
            Trigger(OnGestureStart, gesture);
            switch (gesture.Type) {
                case Gesture.GestureType.TAP: Trigger(OnTapStart, gesture as Tap); break;
                case Gesture.GestureType.SWIPE: Trigger(OnSwipeStart, gesture as Swipe); break;
                case Gesture.GestureType.SPRINCH: Trigger(OnSprinchStart, gesture as Sprinch); break;
                case Gesture.GestureType.PINCH: Trigger(OnPinchStart, gesture as Sprinch); break;
                case Gesture.GestureType.SPREAD: Trigger(OnSpreadStart, gesture as Sprinch); break;
            }
        }

        void NotifyGestureProgress(Gesture gesture) {
            Trigger(OnGestureProgress, gesture);
            switch (gesture.Type) {
                case Gesture.GestureType.SWIPE: Trigger(OnSwipeProgress, gesture as Swipe); break;
                case Gesture.GestureType.SPRINCH: Trigger(OnSprinchProgress, gesture as Sprinch); break;
                case Gesture.GestureType.PINCH: Trigger(OnPinchProgress, gesture as Sprinch); break;
                case Gesture.GestureType.SPREAD: Trigger(OnSpreadProgress, gesture as Sprinch); break;
            }
        }

        void NotifyGestureEnd(Gesture gesture) {
            Trigger(OnGestureEnd, gesture);
            switch (gesture.Type) {
                case Gesture.GestureType.TAP: Trigger(OnTapEnd, gesture as Tap); break;
                case Gesture.GestureType.SWIPE: Trigger(OnSwipeEnd, gesture as Swipe); break;
                case Gesture.GestureType.PINCH: Trigger(OnPinchEnd, gesture as Sprinch); break;
                case Gesture.GestureType.SPREAD: Trigger(OnSpreadEnd, gesture as Sprinch); break;
            }
        }

        static void Trigger<T>(Action<T> handler, T value) {
            if (handler != null) { handler.Invoke(value); }
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
}
