using UnityEngine;
using System;
using Gestures;
using Flyweight;
using Extensions;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public float maxFlowStrength;
        public float flowDuration;
        public ObjectPool flows;
        public new Camera camera;
        public GesturesDispatcher gestures;
        public WorldManager world;

        void Awake() {
            if (maxFlowStrength <= 0) { Debug.LogWarning("FlowGenerator: max flow strength should be a positive value, but it is set to " + maxFlowStrength); }
            if (flowDuration <= 0) { Debug.LogWarning("FlowGenerator: flow duration should be a positive value, but it is set to " + flowDuration); }
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
                FMODUnity.RuntimeManager.PlayOneShot("event:/ambientali/waterflow");
            });
        }
    }
}
