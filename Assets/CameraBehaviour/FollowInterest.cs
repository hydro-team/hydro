using UnityEngine;
using System;
using Extensions;

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
        Vector3 cameraLastAbsolutePosition;

        void Awake() {
            if (approachRate <= 0f || approachRate > 1f) {
                throw new InvalidOperationException("approachRate out of range ]0,1]: " + approachRate);
            }
            camera = GetComponentInChildren<Camera>();
            originalFov = camera.fieldOfView;
        }

        void Update() {
            ForgetPointOfInterestIfOnDifferentLayer();
            ForceCameraPositionIfFollowingPointOfInterest();
            MoveCameraTowardPointOfInterest();
            AdjustCameraZoom();
            SaveCameraLastAbsolutePosition();
        }

        void ForgetPointOfInterestIfOnDifferentLayer() {
            if (currentPointOfInterest == null) { return; }
            var layer = currentPointOfInterest.gameObject.layer;
            if (world.CurrentSlice.layer != layer) { currentPointOfInterest = null; }
        }

        void ForceCameraPositionIfFollowingPointOfInterest() {
            if (currentPointOfInterest != null) { camera.MoveTo(cameraLastAbsolutePosition); }
        }

        void MoveCameraTowardPointOfInterest() {
            Vector2 pointOfInterest = DisplacementOfCurrentPointOfInterest();
            Vector2 cameraPosition = camera.transform.localPosition;
            if (cameraPosition == pointOfInterest) { return; }
            var distance = pointOfInterest - cameraPosition;
            if (distance.sqrMagnitude < 0.0001f) {
                camera.MoveLocallyTo(pointOfInterest);
            } else {
                camera.MoveToward((pointOfInterest - cameraPosition) * approachRate);
            }
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

        void SaveCameraLastAbsolutePosition() {
            cameraLastAbsolutePosition = camera.transform.position;
        }

        void OnTriggerEnter2D(Collider2D other) {
            var point = other.gameObject.GetComponent<PointOfInterest>();
            if (point == null) { return; }
            if (point.gameObject.layer != world.CurrentSlice.layer) { return; }
            currentPointOfInterest = point;
            SaveCameraLastAbsolutePosition();
        }

        void OnTriggerExit2D(Collider2D other) {
            var point = other.gameObject.GetComponent<PointOfInterest>();
            if (point == null) { return; }
            if (point != currentPointOfInterest) { return; }
            currentPointOfInterest = null;
        }
    }
}
