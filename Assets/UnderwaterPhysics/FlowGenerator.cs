using UnityEngine;
using System;
using Gestures;
using Flyweight;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public GameObject flowPool;
        public new Camera camera;
        public GameObject gestures;

        ObjectPool flows;

        void Awake() {
            if (flowPool.GetComponent<ObjectPool>() == null) { throw new OperationCanceledException("Flow generator requires an object pool containing flow objects"); }
            if (gestures.GetComponent<GesturesDispatcher>() == null) { throw new OperationCanceledException("Flow generator requires a GestureDispatcher"); }
            flows = flowPool.GetComponent<ObjectPool>();
            gestures.GetComponent<GesturesDispatcher>().OnSwipeEnd += swipe => GenerateFlow(swipe.Start, swipe.End);
        }

        void GenerateFlow(Vector2 start, Vector2 end) {
            var got = flows.TryRequest(flow => {
                flow.GetComponent<UnderwaterPhysics.Flow>().Enable(
                    from: ScreenToWorld(start),
                    to: ScreenToWorld(end),
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis());
            });
        }

        Vector2 ScreenToWorld(Vector2 position) {
            var cameraDistance = -camera.transform.position.z;
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraDistance));
        }
    }
}
