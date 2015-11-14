using UnityEngine;
using System.Collections;
using Gestures;
using Flyweight;

public class FlowGenerator : MonoBehaviour {

    public GameObject flowPool;
    public new Camera camera;
    public GameObject gestures;

    ObjectPool flows;

    void Start() {
        flows = flowPool.GetComponent<ObjectPool>();
        gestures.GetComponent<GesturesDispatcher>().OnSwipeEnd += swipe => GenerateFlow(swipe.Start, swipe.End);
    }

    void GenerateFlow(Vector2 start, Vector2 end) {
        var got = flows.TryRequest(flow => {
            flow.GetComponent<Physics.Flow>().Enable(
                from: ScreenToWorld(start),
                to: ScreenToWorld(end),
                onExausted: () => flow.GetComponent<SharedObject>().ReleaseThis());
        });
        if (got == null) { Debug.Log("Currently running out of flows"); }
    }

    Vector2 ScreenToWorld(Vector2 position) {
        var cameraDistance = -camera.transform.position.z;
        return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, cameraDistance));
    }
}
