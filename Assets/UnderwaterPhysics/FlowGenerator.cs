using UnityEngine;
using System;
using Gestures;
using Flyweight;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public float maxFlowStrength;
        public float flowDuration;
        public GameObject flowContainer;
        public GameObject flowPool;
        public new Camera camera;
        public GameObject gestures;

        ObjectPool flows;

        void Awake() {
            if (flowPool.GetComponent<ObjectPool>() == null) { throw new OperationCanceledException("Flow generator requires an object pool containing flow objects"); }
            if (gestures.GetComponent<GesturesDispatcher>() == null) { throw new OperationCanceledException("Flow generator requires a GestureDispatcher"); }
            flows = flowPool.GetComponent<ObjectPool>();
            gestures.GetComponent<GesturesDispatcher>().OnSwipeEnd += swipe => GenerateFlow(swipe);
        }

        void GenerateFlow(Swipe swipe) {
            var sharedFlow = flows.TryRequestComponent<Flow>(flow => {
                flow.transform.parent = flowContainer.transform;
                flow.duration = flowDuration;
                flow.strength = maxFlowStrength / (1f + swipe.Duration);
                flow.Enable(
                    from: ScreenToWorld(swipe.Start),
                    to: ScreenToWorld(swipe.End),
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis()
                );
            });
        }

        Vector2 ScreenToWorld(Vector2 position) {
            var cameraDistance = flowContainer.transform.position.z - camera.transform.position.z;
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraDistance));
        }
    }
}
