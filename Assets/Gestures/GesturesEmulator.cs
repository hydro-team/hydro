using UnityEngine;
using System;

namespace Gestures {

    public class GesturesEmulator : MonoBehaviour {

        public float MinDragSpan;

        const int LEFT_BUTTON = 0;

        bool dragging;
        Vector2 dragStartPosition;
        float dragStartTime;
        bool zooming;

        public event Action<Vector2, float> OnClickStart, OnClickEnd;
        public event Action<Vector2, Vector2, float> OnDragStart, OnDragProgress, OnDragEnd;
        public event Action OnZoomStart, OnZoomProgress, OnZoomIn, OnZoomOut;

        void Update() {
            HandleDrag();
            HandleZoom();
        }

        void HandleDrag() {
            if (Input.GetMouseButtonDown(LEFT_BUTTON)) {
                dragging = false;
                dragStartPosition = Input.mousePosition;
                dragStartTime = Time.time;
                if (OnClickStart != null) { OnClickStart(dragStartPosition, 0); }
            }
            Vector2 dragEndPosition = Input.mousePosition;
            var duration = Time.time - dragStartTime;
            if (!dragging && Input.GetMouseButton(LEFT_BUTTON)) {
                var span = (dragEndPosition - dragStartPosition).magnitude;
                if (span >= MinDragSpan) {
                    dragging = true;
                    if (OnDragStart != null) { OnDragStart(dragStartPosition, dragEndPosition, duration); }
                }
            }
            if (Input.GetMouseButtonUp(LEFT_BUTTON)) {
                if (dragging) {
                    if (OnDragEnd != null) { OnDragEnd(dragStartPosition, dragEndPosition, duration); }
                    dragging = false;
                } else {
                    if (OnClickEnd != null) { OnClickEnd(dragStartPosition, duration); }
                }
            } else if (dragging) {
                if (OnDragProgress != null) { OnDragProgress(dragStartPosition, dragEndPosition, duration); }
            }
        }

        void HandleZoom() {
            if (zooming && Input.GetKeyUp(KeyCode.LeftShift)) {
                if (Input.GetKey(KeyCode.W)) { if (OnZoomIn != null) { OnZoomIn(); } }
                if (Input.GetKey(KeyCode.S)) { if (OnZoomOut != null) { OnZoomOut(); } }
                zooming = false;
            } else if (zooming) {
                if (OnZoomProgress != null) { OnZoomProgress(); }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                if (OnZoomStart != null) { OnZoomStart(); }
                zooming = true;
            }
        }
    }
}
