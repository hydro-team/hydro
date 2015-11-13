using UnityEngine;
using System;

namespace Gestures {

    public class GesturesRecogniser : MonoBehaviour {

        public event Action<Gesture> GestureStart;
        public event Action<Gesture> GestureProgress;
        public event Action<Gesture> GestureEnd;

        const float MAX_DISTANCE_BEFORE_SWIPE = 20f;

        GestureState gestureState = GestureState.NEUTRAL;
        Gesture currentGesture;

        public GestureState CurrentState {
            get { return gestureState; }
        }

        void Update() {
            switch (gestureState) {
                case GestureState.TAP: Tap(); break;
                case GestureState.SWIPE: Swipe(); break;
                case GestureState.SPRINCH: Sprinch(); break;
                default: BeginRecognition(); break;
            }
        }

        bool NoTouch() { return Input.touches.Length == 0; }
        bool SingleTouch() { return Input.touches.Length == 1; }
        bool MultiTouch() { return Input.touches.Length > 1; }

        /// <summary>Begins the recognition of the current gesture.</summary>
        void BeginRecognition() {
            if (SingleTouch()) {
                if (Input.touches[0].phase == TouchPhase.Began) {
                    currentGesture = new Tap(Input.touches[0].position, Time.time);
                    if (gestureState == GestureState.NEUTRAL) { NotifyGestureRecognitionStart(); }
                    gestureState = GestureState.TAP;
                }
            } else if (MultiTouch()) {
                currentGesture = new Sprinch(Input.touches[0].position, Input.touches[1].position);
                gestureState = GestureState.SPRINCH;
            }
        }

        /// <summary>Recognising a tap gesture.</summary>
        void Tap() {
            var tap = currentGesture as Tap;
            if (NoTouch()) {
                NotifyGetsureRecognitionEnd();
                return;
            }
            if (SingleTouch() && Vector2.Distance(Input.touches[0].position, tap.Position) > MAX_DISTANCE_BEFORE_SWIPE) {
                currentGesture = new Swipe(tap.Position);
                if (gestureState == GestureState.TAP) { NotifyGestureRecognitionStart(); }
                gestureState = GestureState.SWIPE;
                //Il caso limite in cui lo swipe viene riconosciuto nello stesso momento in cui finisce è escluso
                return;
            }
            //TODO pinch&Spread
            //FIXME
            if (MultiTouch()) {
                currentGesture = new Sprinch(tap.Position, Input.touches[1].position);
                gestureState = GestureState.SPRINCH;
            }
        }

        /// <summary>Recognising the swipe gesture.</summary>
        void Swipe() {
            //FIXME
            var swipe = currentGesture as Swipe;
            if (MultiTouch()) {
                currentGesture = new Sprinch(swipe.Start, Input.touches[1].position);
                gestureState = GestureState.SPRINCH;
                return;
            }
            if (NoTouch()) {
                NotifyGetsureRecognitionEnd();
                return;
            }
            if (Input.touches[0].phase == TouchPhase.Ended) {
                swipe.End = Input.touches[0].position;
                NotifyGetsureRecognitionEnd();
                return;
            }
            if (Input.touches[0].phase == TouchPhase.Moved) {
                swipe.End = Input.touches[0].position;
                NotifyGestureRecognitionProgress();
                return;
            }
        }

        void Sprinch() {
            var sprinch = currentGesture as Sprinch;
            if (!MultiTouch()) {
                NotifyGetsureRecognitionEnd();
                return;
            }
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[1].phase == TouchPhase.Ended) {
                Vector2[] points = new Vector2[2] { Input.touches[0].position, Input.touches[1].position };
                sprinch.EndPoints = points;
                NotifyGetsureRecognitionEnd();
                return;
            }
            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved) {
                var previousType = sprinch.Type;
                sprinch.EndPoints = new Vector2[2] { Input.touches[0].position, Input.touches[1].position };
                if (previousType == GestureType.SPRINCH && currentGesture.Type != GestureType.SPRINCH) {
                    NotifyGestureRecognitionStart();
                }
                NotifyGestureRecognitionProgress();
                return;
            }
        }

        void NotifyGestureRecognitionStart() {
            if (GestureStart != null) GestureStart(currentGesture);
        }

        void NotifyGestureRecognitionProgress() {
            if (GestureProgress != null) { GestureProgress(currentGesture); }
        }

        void NotifyGetsureRecognitionEnd() {
            if (GestureEnd != null) GestureEnd(currentGesture);
            gestureState = GestureState.NEUTRAL;
        }

        public enum GestureState {
            SWIPE,
            SPRINCH,
            SPREAD,
            PINCH,
            SPREAD_END,
            PINCH_END,
            PINSPR_NEUTRAL,
            TAP,
            NEUTRAL
        }
    }
}
