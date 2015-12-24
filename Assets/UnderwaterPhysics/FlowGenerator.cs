using UnityEngine;
using Gestures;
using Flyweight;
using Extensions;
using Sounds;

namespace UnderwaterPhysics {

    public class FlowGenerator : MonoBehaviour {

        public float maxFlowStrength;
        public float flowDuration;
        public ObjectPool flows;
        public new Camera camera;
        public GesturesDispatcher gestures;
        public WorldManager world;
        public GameObject soundFacade;

        SoundFacade sounds;

        void Awake() {
            if (maxFlowStrength <= 0) { Debug.LogWarning("FlowGenerator: max flow strength should be a positive value, but it is set to " + maxFlowStrength); }
            if (flowDuration <= 0) { Debug.LogWarning("FlowGenerator: flow duration should be a positive value, but it is set to " + flowDuration); }
            gestures.OnSwipeEnd += GenerateFlow;
            sounds = soundFacade.GetComponent<SoundFacade>();
        }

        void GenerateFlow(Swipe swipe) {
            var sharedFlow = flows.TryRequestComponent<Flow>(flow => {
                flow.duration = flowDuration;
                flow.strength = maxFlowStrength / (1f + swipe.Duration);
                flow.gameObject.layer = world.CurrentSlice.layer;
                var z = world.character.transform.position.z;
                flow.Enable(
                    from: camera.ScreenToWorldPoint(swipe.Start, z),
                    to: camera.ScreenToWorldPoint(swipe.End, z),
                    z: z,
                    onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis()
                );
                sounds.Play("/ambientali/waterflow");
            });
        }
    }
}
