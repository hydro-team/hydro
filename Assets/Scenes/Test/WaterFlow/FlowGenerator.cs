using UnityEngine;
using Gestures;

public class FlowGenerator : MonoBehaviour {

    public GameObject DecayingFlowPrototype;
    public GameObject MainCamera;

    Camera camera;

    void Start() {
        camera = Camera.main;
        GesturesDispatcher.OnSwipeEnd += swipe => GenerateFlow(swipe.Start, swipe.End);
    }

    void GenerateFlow(Vector2 start, Vector2 end) {
        var rotation = Quaternion.FromToRotation(Vector3.up, end - start);
        var cameraDistance = -camera.transform.position.z;
        Vector2 from = camera.ScreenToWorldPoint(new Vector3(start.x, start.y, cameraDistance));
        Vector2 to = camera.ScreenToWorldPoint(new Vector3(end.x, end.y, cameraDistance));
        var flowObject = GameObject.Instantiate(DecayingFlowPrototype, (from + to) / 2f, rotation) as GameObject;
        flowObject.transform.localScale = new Vector3(1f, (to - from).magnitude / 2f, 1f);
    }
}
