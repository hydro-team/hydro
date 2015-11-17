using UnityEngine;
using System;
using Gestures;
using Flyweight;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public float maxFlowStrength;
        public float flowDuration;
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
            flows.TryRequest(flow => {
                var f = flow.GetComponent<Flow>();
                f.duration = flowDuration;
                f.strength = maxFlowStrength / (1f + swipe.Duration);
                f.Enable(
                    from: ScreenToWorld(swipe.Start),
                    to: ScreenToWorld(swipe.End),
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis());
            });
        }

        Vector2 ScreenToWorld(Vector2 position) {
            var cameraDistance = -camera.transform.position.z;
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraDistance));
        }
    }
}
