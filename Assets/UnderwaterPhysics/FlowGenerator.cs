using UnityEngine;
using System;
using Gestures;
using Flyweight;
using Extensions;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public float maxFlowStrength;
        public float flowDuration;
        public GameObject flowPool;
        public new Camera camera;
        public GesturesDispatcher gestures;
        public WorldManager world;

        ObjectPool flows;

        void Awake() {
            if (flowPool.GetComponent<ObjectPool>() == null) { throw new OperationCanceledException("Flow generator requires an object pool containing flow objects"); }
            flows = flowPool.GetComponent<ObjectPool>();
            gestures.OnSwipeEnd += GenerateFlow;
        }

        void GenerateFlow(Swipe swipe) {
            var sharedFlow = flows.TryRequestComponent<Flow>(flow => {
                flow.duration = flowDuration;
                flow.strength = maxFlowStrength / (1f + swipe.Duration);
                flow.gameObject.layer = world.CurrentSlice.layer;
                var z = world.CurrentSlice.transform.position.z;
                flow.Enable(
                    from: camera.ScreenToWorldPoint(swipe.Start, z),
                    to: camera.ScreenToWorldPoint(swipe.End, z),
                    z: world.CurrentSliceZ,
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis()
                );
            });
        }
    }
}
