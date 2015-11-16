using UnityEngine;
using System.Collections;

namespace Gestures {

    public class Tap : Gesture {

        Vector2 position;
        float startTime;

        public Vector2 Position { get { return position; } }

        public float BeginTime { get { return startTime; } }

        public Tap(Vector2 position, float startTime) : base(GestureType.TAP) {
            this.position = position;
            this.startTime = startTime;

        }

        public float Duration() {
            return Time.time - startTime;
        }
    }
}
