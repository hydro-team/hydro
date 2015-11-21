using UnityEngine;
using System;

namespace Extensions {

    public static class CameraExtensions {

        /// <summary>
        /// Returns the point in world coordinates that matches the given screen point at the target z.
        /// It works only for 2D cameras always facing the positive z direction.
        /// </summary>
        public static Vector2 ScreenToWorldPoint(this Camera camera, Vector2 screenPoint, float targetZ) {
            var cameraDistance = targetZ - camera.transform.position.z;
            if (cameraDistance < 0) { throw new InvalidOperationException("target z is not in front of the camera"); }
            return camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, cameraDistance));
        }
    }
}
