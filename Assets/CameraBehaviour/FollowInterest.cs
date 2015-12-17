using UnityEngine;
using System;

namespace CameraBehaviour {

    /// <summary>
    /// Moves the child camera toward a point of interest when inside of the interest area.
    /// When not following a point of interest, the camera follows this object.
    ///
    /// The approach rate is the proportion of distance covered by the camera toward the point of interest each frame.
    /// An approach rate of 1 means the immediate placement of the camera over the point of interest.
    /// Approach rates near to 0 give a smooth transition.
    ///
    /// Only points of interest on the same layer of the camera are considered.
    /// When changing layer, the current point of interest is forget.
    /// </summary>
    public class FollowInterest : MonoBehaviour {

        public WorldManager world;
        public float approachRate;

        new Camera camera;
        float originalFov;
        PointOfInterest currentPointOfInterest;

        void Awake() {
            if (approachRate <= 0f || approachRate > 1f) {
                throw new InvalidOperationException("approachRate out of range ]0,1]: " + approachRate);
            }
            camera = GetComponentInChildren<Camera>();
            originalFov = camera.fieldOfView;
        }

        void Update() {
            ForgetPointOfInterestIfOnDifferentLayer();
            MoveCameraTowardPointOfInterest();
            AdjustCameraZoom();
        }

        void ForgetPointOfInterestIfOnDifferentLayer() {
            if (currentPointOfInterest == null) { return; }
            var layer = currentPointOfInterest.gameObject.layer;
            if (world.CurrentSlice.layer != layer) { currentPointOfInterest = null; }
        }

        void MoveCameraTowardPointOfInterest() {
            var displacement = DisplacementOfCurrentPointOfInterest();
            var cameraPosition = camera.transform.localPosition;
            if ((Vector2)cameraPosition == displacement) { return; }
            var distance = displacement - (Vector2)cameraPosition;
            if (distance.sqrMagnitude < 0.0001f) {
                camera.transform.localPosition = new Vector3(displacement.x, displacement.y, cameraPosition.z);
                return;
            }
            Vector3 movement = (displacement - (Vector2)cameraPosition) * approachRate;
            camera.transform.localPosition += movement;
        }

        Vector2 DisplacementOfCurrentPointOfInterest() {
            if (currentPointOfInterest == null) { return Vector2.zero; }
            return currentPointOfInterest.transform.position - transform.position;
        }

        void AdjustCameraZoom() {
            var targetFov = TargetFov();
            var fovDifference = targetFov - camera.fieldOfView;
            if (Mathf.Abs(fovDifference) < 0.01f) {
                camera.fieldOfView = targetFov;
            } else {
                camera.fieldOfView += fovDifference * approachRate;
            }
        }

        float TargetFov() {
            return currentPointOfInterest == null ? originalFov : originalFov + currentPointOfInterest.fovDelta;
        }

        void OnTriggerEnter2D(Collider2D other) {
            var point = other.gameObject.GetComponent<PointOfInterest>();
            if (point == null) { return; }
            if (point.gameObject.layer != world.CurrentSlice.layer) { return; }
            currentPointOfInterest = point;
        }

        void OnTriggerExit2D(Collider2D other) {
            var point = other.gameObject.GetComponent<PointOfInterest>();
            if (point == null) { return; }
            if (point != currentPointOfInterest) { return; }
            currentPointOfInterest = null;
        }
    }
}
