using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CameraBehaviour {

    public class FollowInterest : MonoBehaviour {

        public WorldManager world;
        public float captureDistance;
        public float speedRate;

        new Camera camera;
        IDictionary<int, IList<PointOfInterest>> pointsOfInterest;
        Vector2 currentRelativePoint;
        float originalFov;
        float targetFov;

        void Awake() {
            if (speedRate <= 0f || speedRate > 1f) {
                throw new InvalidOperationException("speedRate out of range ]0,1]: " + speedRate);
            }
            camera = GetComponent<Camera>();
            originalFov = camera.fieldOfView;
            targetFov = originalFov;
            LookupPointsOfInterestByLayer();
        }

        void LookupPointsOfInterestByLayer() {
            pointsOfInterest = FindObjectsOfType<PointOfInterest>()
                .GroupBy(point => point.gameObject.layer)
                .ToDictionary(group => group.Key, group => (IList<PointOfInterest>)group.ToList());
        }

        void Update() {
            SearchClosestPointOfInterest();
            MoveTowardPointOfInterest();
            Zoom();
        }

        void SearchClosestPointOfInterest() {
            var layer = world.CurrentSlice.layer;
            if (!pointsOfInterest.ContainsKey(layer)) {
                SetDefaultRelativePointAndZoom();
                return;
            }
            var pointOfInterest = pointsOfInterest[layer]
                .Where(CloseEnough())
                .OrderBy(point => RelativePosition(point).sqrMagnitude)
                .FirstOrDefault();
            if (pointOfInterest == null) {
                SetDefaultRelativePointAndZoom();
            } else {
                currentRelativePoint = RelativePosition(pointOfInterest);
                targetFov = originalFov + pointOfInterest.fovDifference;
            }
        }

        void MoveTowardPointOfInterest() {
            if ((Vector2)transform.localPosition == currentRelativePoint) { return; }
            var distance = currentRelativePoint - (Vector2)transform.localPosition;
            if (distance.sqrMagnitude < 0.0001f) {
                transform.localPosition = new Vector3(currentRelativePoint.x, currentRelativePoint.y, transform.localPosition.z);
                return;
            }
            Vector3 movement = (currentRelativePoint - (Vector2)transform.localPosition) * speedRate;
            transform.localPosition += movement;
        }

        void Zoom() {
            var fovDifference = targetFov - camera.fieldOfView;
            if (fovDifference < 0.001) {
                camera.fieldOfView = targetFov;
                return;
            }
            camera.fieldOfView += fovDifference * speedRate;
        }

        Vector2 RelativePosition(PointOfInterest point) {
            var position = (Vector2)transform.parent.position;
            return (Vector2)point.transform.position - position;
        }

        Func<PointOfInterest, bool> CloseEnough() {
            var sqrCaptureDistance = captureDistance * captureDistance;
            return point => RelativePosition(point).sqrMagnitude < sqrCaptureDistance;
        }

        void SetDefaultRelativePointAndZoom() {
            currentRelativePoint = Vector2.zero;
            targetFov = originalFov;
        }
    }
}
