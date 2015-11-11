using UnityEngine;
using Gestures;
using Flyweight;

public class FlowGenerator : MonoBehaviour {

    public GameObject FlowPool;
    public GameObject MainCamera;

    ObjectPool flows;

    void Start() {
        flows = FlowPool.GetComponent<ObjectPool>();
        GesturesDispatcher.OnSwipeEnd += swipe => GenerateFlow(swipe.Start, swipe.End);
    }

    void GenerateFlow(Vector2 start, Vector2 end) {
        var camera = Camera.main;
        var rotation = Quaternion.FromToRotation(Vector3.up, end - start);
        var cameraDistance = -camera.transform.position.z;
        Vector2 from = camera.ScreenToWorldPoint(new Vector3(start.x, start.y, cameraDistance));
        Vector2 to = camera.ScreenToWorldPoint(new Vector3(end.x, end.y, cameraDistance));
        var got = flows.TryRequest(flow => {
            flow.GetComponent<DecayingFlow>().Initialize(from, to, () => {
                flow.GetComponent<SharedObject>().ReleaseThis();
            });
        });
        if (got == null) { Debug.Log("Currently running out of flows"); }
    }
}
