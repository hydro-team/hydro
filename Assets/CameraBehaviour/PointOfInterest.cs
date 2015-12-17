using UnityEngine;
using System;

namespace CameraBehaviour {

    /// <summary>
    /// Defines a target position and FOV (field of view) for the camera when it enters the zone of this point of interest.
    /// The target position is given by the position of the game object to which this component is attached.
    /// The target FOV is the sum of the original FOV of the camera and the specified delta.
    /// The zone where this point of interest should capture the camera's attention is defined by a collider 2D that must be a trigger.
    ///
    /// When setting the FOV delta, consder that the camera should be capable of viewing the entire interest zone.
    /// Otherwise, the main character could get out of the screen and become unreachable.
    ///
    /// Points of interest should be disabled or removed when they become not interesting anymore.
    /// </summary>
    public class PointOfInterest : MonoBehaviour {

        public float fovDelta = 0;

        void Awake() {
            var collider = GetComponent<Collider2D>();
            if (collider == null || !collider.isTrigger) {
                throw new InvalidOperationException("A point of interest must have a trigger collider attached");
            }
        }
    }
}
