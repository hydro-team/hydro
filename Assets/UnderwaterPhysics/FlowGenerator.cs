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
                flow.Enable(
                    from: ScreenToWorld(swipe.Start),
                    to: ScreenToWorld(swipe.End),
                    z: world.CurrentSlice.transform.position.z,
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis()
                );
            });
        }

        Vector2 ScreenToWorld(Vector2 position) {
            var cameraDistance = Math.Abs(world.CurrentSlice.transform.position.z - camera.transform.position.z);
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraDistance));
        }
    }
}
