using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CameraBehaviour {

    public class FollowInterest : MonoBehaviour {

        public WorldManager world;
        public float captureDistance;
        public float speedRate;

        IDictionary<int, IList<PointOfInterest>> pointsOfInterest;
        Vector2 currentRelativePoint;

        void Awake() {
            if (speedRate <= 0f || speedRate > 1f) {
                throw new InvalidOperationException("speedRate out of range ]0,1]: " + speedRate);
            }
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
        }

        void SearchClosestPointOfInterest() {
            currentRelativePoint = pointsOfInterest[world.CurrentSlice.layer]
                .Select(RelativePosition())
                .Where(CloseEnough())
                .OrderBy(relativePosition => relativePosition.sqrMagnitude)
                .FirstOrDefault();
        }

        void MoveTowardPointOfInterest() {
            if ((Vector2) transform.localPosition == currentRelativePoint) { return; }
            var distance = currentRelativePoint - (Vector2)transform.localPosition;
            if (distance.sqrMagnitude < 0.0001f) {
                transform.localPosition = new Vector3(currentRelativePoint.x, currentRelativePoint.y, transform.localPosition.z);
                return;
            }
            Vector3 movement = (currentRelativePoint - (Vector2)transform.localPosition) * speedRate;
            transform.localPosition += movement;
        }

        Func<PointOfInterest, Vector2> RelativePosition() {
            var position = (Vector2)transform.parent.position;
            return point => (Vector2)point.transform.position - position;
        }

        Func<Vector2, bool> CloseEnough() {
            var sqrCaptureDistance = captureDistance * captureDistance;
            return relativePosition => relativePosition.sqrMagnitude < sqrCaptureDistance;
        }
    }
}
